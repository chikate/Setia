using Main.Data.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using System.IO.Compression;

namespace Main.Services;

public interface IFileManagerService
{
    Task<bool> CreateFolder(string filePath);
    Task DeleteFFC(string filePath);
    Task<FileContentResult> DownloadFFC(string filePath);
    Task<List<DriveInfoDTO>> GetAllPartitions();
    Task<string?> GetFileFromRegistryNumber(Guid registryNumber);
    Task<List<string>> GetFolderContent(string filePath);
    Task<Guid> GetFileRegistryNumber(string filePath);
    Task<bool> IsFileCompressed(IFormFile formFile);
    Task<string> UploadFFC(IFormFile formFile, bool compressIt);
}

public class FileManagerService : IFileManagerService
{
    #region Dependency Injection
    private readonly IWebHostEnvironment _env;
    private readonly ILogger<SenderService> _logger;
    private readonly IConfiguration _config;

    private readonly Dictionary<Guid, string> _registryCache = new Dictionary<Guid, string>();
    private string _lastCheckedFile = "";
    private const int _registryNumberByteSize = 16; // Size of a GUID in bytes
    private const long _maxFileSizeBytes = 50 * 1024 * 1024; // 50 MB

    public FileManagerService
    (
        IWebHostEnvironment env,
        ILogger<SenderService> logger,
        IConfiguration config
    )
    {
        _env = env;
        _logger = logger;
        _config = config;
    }
    #endregion

    #region File Manager CRUD
    // FFC - File from Cloud
    public async Task<string> UploadFFC(IFormFile formFile, bool compressIt)
    {
        if (formFile == null || formFile.Length == 0) throw new ArgumentNullException(nameof(formFile), "File cannot be null or empty.");
        if (!Directory.Exists(_env.WebRootPath)) Directory.CreateDirectory(_env.WebRootPath);
        string filePath = Path.Combine(_env.WebRootPath, formFile.FileName);
        //if (File.Exists(filePath)) throw new IOException($"A file with the name '{formFile.FileName}' already exists.");
        using (var stream = new FileStream(filePath, FileMode.Create)) { await formFile.CopyToAsync(stream); }
        await AppendRegistryNumberToFile(filePath);
        if (!(await IsFileCompressed(formFile)) && (compressIt || formFile.Length > _maxFileSizeBytes))
        {
            string zipFilePath = Path.ChangeExtension(filePath, ".zip");
            using (FileStream zipToAddTo = new FileStream(zipFilePath, FileMode.Create))
            using (ZipArchive archive = new ZipArchive(zipToAddTo, ZipArchiveMode.Update))
            { archive.CreateEntryFromFile(filePath, Path.GetFileName(filePath)); }
            File.Delete(filePath);
            filePath = zipFilePath;
        }
        return filePath;
    }
    public async Task<FileContentResult> DownloadFFC(string filePath)
    {
        if (!new FileExtensionContentTypeProvider().TryGetContentType(filePath, out var contentType)) contentType = "application/octet-stream";
        return new FileContentResult(await File.ReadAllBytesAsync(Path.Combine(_env.WebRootPath, filePath)), contentType) { FileDownloadName = Path.GetFileName(filePath) };
    }
    public async Task DeleteFFC(string filePath)
    {
        filePath = Path.Combine(_env.WebRootPath, filePath);
        if (!File.Exists(filePath)) throw new FileNotFoundException("File does not exist", filePath);
        File.Delete(filePath);
        await Task.CompletedTask;
    }
    #endregion

    #region File Registry Manager
    private async Task AppendRegistryNumberToFile(string filePath, Guid? registryNumber = null)
    {
        byte[] registryNumberBytes = (registryNumber ?? Guid.NewGuid()).ToByteArray();
        using (var stream = new FileStream(filePath, FileMode.Append, FileAccess.Write))
        { await stream.WriteAsync(registryNumberBytes, 0, registryNumberBytes.Length); }
    }
    public async Task<Guid> GetFileRegistryNumber(string filePath)
    {
        filePath = Path.Combine(_env.WebRootPath, filePath);
        using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
        {
            stream.Seek(-_registryNumberByteSize, SeekOrigin.End);
            var buffer = new byte[_registryNumberByteSize];
            await stream.ReadAsync(buffer, 0, _registryNumberByteSize);
            return new Guid(buffer);
        }
    }
    public async Task<string?> GetFileFromRegistryNumber(Guid registryNumber)
    {
        byte[] targetBytes = registryNumber.ToByteArray();
        string[] files = Directory.GetFiles(_env.WebRootPath, "*", SearchOption.AllDirectories);
        int start = string.IsNullOrEmpty(_lastCheckedFile) ? 0 : (Array.IndexOf(files, _lastCheckedFile) + 1) % files.Length;

        for (int i = 0; i < files.Length; i++)
        {
            var file = files[(start + i) % files.Length];
            if (new FileInfo(file).Length < _registryNumberByteSize) continue;

            using var stream = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.Read);
            stream.Seek(-_registryNumberByteSize, SeekOrigin.End);
            var buffer = new byte[_registryNumberByteSize];
            await stream.ReadAsync(buffer, 0, _registryNumberByteSize);

            if (buffer.SequenceEqual(targetBytes))
                return _registryCache[registryNumber] = _lastCheckedFile = file;

            _lastCheckedFile = file;
        }

        return null;
    }
    #endregion

    public async Task<bool> IsFileCompressed(IFormFile formFile)
    {
        const int signatureLength = 6; // Enough to check most signatures
        byte[] signatureBuffer = new byte[signatureLength];
        using (var stream = formFile.OpenReadStream()) { stream.Read(signatureBuffer, 0, signatureLength); }

        await Task.CompletedTask;
        return  // Additional checks can be added for TAR if needed
                // Check for ZIP signature "PK\x03\x04"
                (signatureBuffer[0] == 0x50 && signatureBuffer[1] == 0x4B && signatureBuffer[2] == 0x03 && signatureBuffer[3] == 0x04)
                // Check for RAR signature "Rar!"
                || (signatureBuffer[0] == 0x52 && signatureBuffer[1] == 0x61 && signatureBuffer[2] == 0x72 && signatureBuffer[3] == 0x21)
                // Check for GZ signature "\x1F\x8B"
                || (signatureBuffer[0] == 0x1F && signatureBuffer[1] == 0x8B)
                // Check for 7Z signature "7z"
                || (signatureBuffer[0] == 0x37 && signatureBuffer[1] == 0x7A && signatureBuffer[2] == 0xBC && signatureBuffer[3] == 0xAF);
    }
    public async Task<List<string>> GetFolderContent(string filePath)
    {
        if (filePath.StartsWith("/")) filePath = filePath.Substring(1);
        filePath = Path.Combine(_env.WebRootPath, filePath);

        if (string.IsNullOrWhiteSpace(filePath) || !Directory.Exists(filePath))
            throw new DirectoryNotFoundException("The specified directory does not exist.");

        List<string> contentList = new();
#pragma warning disable CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
        contentList.AddRange(Directory.GetDirectories(filePath).Select(Path.GetFileName));
        contentList.AddRange(Directory.GetFiles(filePath).Select(Path.GetFileName));
#pragma warning restore CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.

        // Optionally, you can map them to just the names if needed
        //contentList = contentList.ConvertAll(Path.GetFileName);

        await Task.CompletedTask;
        return contentList;
    }
    public async Task<List<DriveInfoDTO>> GetAllPartitions()
    {
        List<DriveInfoDTO> driveDescriptions = new();

        foreach (var drive in DriveInfo.GetDrives())
            if (drive.IsReady)
                driveDescriptions.Add(new DriveInfoDTO
                {
                    Name = drive.Name,
                    VolumeLabel = drive.VolumeLabel,
                    DriveType = drive.DriveType.ToString(),
                    AvailableFreeSpace = drive.AvailableFreeSpace,
                    TotalSize = drive.TotalSize
                });

        await Task.CompletedTask;
        return driveDescriptions;
    }
    public async Task<bool> CreateFolder(string filePath)
    {
        filePath = Path.Combine(_env.WebRootPath, filePath);

        if (Directory.Exists(filePath))
            throw new Exception("Folder already exists, return false or handle as needed");

        Directory.CreateDirectory(filePath);

        await Task.CompletedTask;
        return true;
    }
}