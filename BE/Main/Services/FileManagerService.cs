using Main.Data.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using System.IO.Compression;

namespace Main.Services;

public interface IFileManagerService
{
    Task<bool> CreateFolder(string filePath);
    Task DeleteFile(string filePath);
    Task<FileContentResult> Download(string filePath);
    Task<List<DriveInfoDto>> GetAllPartitions();
    Task<List<string>> GetFolderContent(string filePath);
    Task<bool> IsFileCompressed(IFormFile formFile);
    Task<Guid> GetRegistryNumberFromFile(string filePath);
    Task<string> Upload(IFormFile formFile, bool compressIt);
}

public class FileManagerService : IFileManagerService
{
    #region Dependency Injection
    private readonly IWebHostEnvironment _env;
    private readonly ILogger<SenderService> _logger;
    private readonly IConfiguration _config;


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

    public async Task<string> Upload(IFormFile formFile, bool compressIt)
    {
        if (formFile == null || formFile.Length == 0) throw new ArgumentNullException(nameof(formFile), "File cannot be null or empty.");
        if (!Directory.Exists(_env.WebRootPath)) Directory.CreateDirectory(_env.WebRootPath);
        string filePath = Path.Combine(_env.WebRootPath, formFile.FileName);
        //if (File.Exists(filePath)) throw new IOException($"A file with the name '{formFile.FileName}' already exists.");
        using (var stream = new FileStream(filePath, FileMode.Create)) { await formFile.CopyToAsync(stream); }
        // Append the registry number as bytes to the end of the file - Create regestry number in db and send it to the file
        await AppendRegistryNumberToFile(filePath, Guid.NewGuid());
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
    public async Task<FileContentResult> Download(string filePath)
    {
        if (!new FileExtensionContentTypeProvider().TryGetContentType(filePath, out var contentType)) contentType = "application/octet-stream";
        return new FileContentResult(await File.ReadAllBytesAsync(Path.Combine(_env.WebRootPath, filePath)), contentType) { FileDownloadName = Path.GetFileName(filePath) };
    }
    public async Task<bool> IsFileCompressed(IFormFile formFile)
    {
        const int signatureLength = 6; // Enough to check most signatures
        byte[] signatureBuffer = new byte[signatureLength];
        using (var stream = formFile.OpenReadStream()) { stream.Read(signatureBuffer, 0, signatureLength); }

        // Simulate async operation
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
        filePath = Path.Combine(_env.WebRootPath, filePath);

        if (string.IsNullOrWhiteSpace(filePath) || !Directory.Exists(filePath))
            throw new DirectoryNotFoundException("The specified directory does not exist.");

        List<string> contentList = new();
        contentList.AddRange(Directory.GetDirectories(filePath).Select(Path.GetFileName));
        contentList.AddRange(Directory.GetFiles(filePath).Select(Path.GetFileName));

        // Optionally, you can map them to just the names if needed
        //contentList = contentList.ConvertAll(Path.GetFileName);

        // Simulate async operation
        await Task.CompletedTask;
        return contentList;
    }
    public async Task<List<DriveInfoDto>> GetAllPartitions()
    {
        List<DriveInfoDto> driveDescriptions = new();

        foreach (var drive in DriveInfo.GetDrives())
            if (drive.IsReady)
                driveDescriptions.Add(new DriveInfoDto
                {
                    Name = drive.Name,
                    VolumeLabel = drive.VolumeLabel,
                    DriveType = drive.DriveType.ToString(),
                    AvailableFreeSpace = drive.AvailableFreeSpace,
                    TotalSize = drive.TotalSize
                });

        // Simulate async operation
        await Task.CompletedTask;
        return driveDescriptions;
    }
    public async Task<bool> CreateFolder(string filePath)
    {
        filePath = Path.Combine(_env.WebRootPath, filePath);

        if (Directory.Exists(filePath))
            throw new Exception("Folder already exists, return false or handle as needed");

        Directory.CreateDirectory(filePath);

        // Simulate async operation 
        await Task.CompletedTask;
        return true;
    }
    public async Task DeleteFile(string filePath)
    {
        filePath = Path.Combine(_env.WebRootPath, filePath);
        if (!File.Exists(filePath)) throw new FileNotFoundException("File does not exist", filePath);
        File.Delete(filePath);

        // Simulate async operation
        await Task.CompletedTask;
    }
    private async Task AppendRegistryNumberToFile(string filePath, Guid registryNumber)
    {
        byte[] registryNumberBytes = registryNumber.ToByteArray();
        using (var stream = new FileStream(filePath, FileMode.Append, FileAccess.Write))
        { await stream.WriteAsync(registryNumberBytes, 0, registryNumberBytes.Length); }
    }
    public async Task<Guid> GetRegistryNumberFromFile(string filePath)
    {
        filePath = Path.Combine(_env.WebRootPath, filePath);
        const int registryNumberSize = 16; // Size of a GUID in bytes
        using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
        {
            // Move the stream to the end minus the size of the GUID
            stream.Seek(-registryNumberSize, SeekOrigin.End);
            var buffer = new byte[registryNumberSize];
            await stream.ReadAsync(buffer, 0, registryNumberSize);
            return new Guid(buffer);
        }
    }
}