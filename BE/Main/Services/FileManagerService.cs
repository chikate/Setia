using Main.Data.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.VisualBasic.FileIO;
using System.Text.RegularExpressions;

namespace Main.Services;

public interface IFileManagerService
{
    Task<IFormFile> AppendRegistryToFile(IFormFile formFile);
    Task AppendRegistryToFile(string filePath);
    Task<bool> CreateFolder(string filePath);
    Task Delete(string filePath);
    Task<FileContentResult> Download(string filePath);
    Task<string?> FindFileByName(string fileName);
    Task<List<DriveInfoDTO>> GetAllPartitions();
    Task<string?> GetFileFromRegistry(Guid registry);
    Task<Guid> GetFileRegistry(string filePath);
    Task<List<string>> GetFolderContent(string filePath);
    byte[] GetNewRegistryAsBytes();
    bool IsFileCompressed(IFormFile formFile);
    Task<string?> SearchAndGetFile(string input);
    Task<string> Upload(IFormFile formFile, string? saveToPath);
}

public class FileManagerService : IFileManagerService
{
    #region Dependency Injection
    private readonly IWebHostEnvironment _env;
    private readonly ILogger<SenderService> _logger;
    private readonly IConfiguration _config;
    private readonly IAuthService _auth;

    private readonly Dictionary<Guid, string> _registryCache = new Dictionary<Guid, string>();
    private string _lastCheckedFile = "";
    private const int _registryByteSize = 16; // Size of a GUID in bytes
    private const long _maxFileSizeBytes = 50 * 1024 * 1024; // 50 MB

    public FileManagerService
    (
        IWebHostEnvironment env,
        ILogger<SenderService> logger,
        IConfiguration config,
        IAuthService auth
    )
    {
        _env = env;
        _logger = logger;
        _config = config;
        _auth = auth;
    }
    #endregion

    #region File Manager CRUD
    public async Task<string> Upload(IFormFile formFile, string? saveToPath)
    {
        if (formFile == null || formFile.Length == 0)
            throw new ArgumentNullException(nameof(formFile), "Form file cannot be null or empty.");

        saveToPath = string.IsNullOrEmpty(saveToPath) ? (await _auth.GetCurrentUser())?.Id.ToString() : CleanUpPath(saveToPath);

        // Ensure directory exists
        Directory.CreateDirectory(Path.Combine(_env.WebRootPath, saveToPath!));

        saveToPath = Path.Combine(_env.WebRootPath, Path.Combine(saveToPath!, formFile.FileName));

        await using FileStream fileStream = new FileStream(saveToPath, FileMode.Create);
        await (await AppendRegistryToFile(formFile)).CopyToAsync(fileStream);

        return saveToPath;

        //if (IsFileCompressed(formFile)) return saveToPath;

        //string zipFilePath = Path.ChangeExtension(saveToPath, ".zip");
        //await using (var zipStream = new FileStream(zipFilePath, FileMode.Create))
        //await using (var archive = new ZipArchive(zipStream, ZipArchiveMode.Update))
        //    archive.CreateEntryFromFile(saveToPath, Path.GetFileName(saveToPath));

        //File.Delete(saveToPath);
        //return zipFilePath;
    }
    public async Task<FileContentResult> Download(string filePath)
    {
        filePath = CleanUpPath(filePath);

        if (!new FileExtensionContentTypeProvider().TryGetContentType(filePath, out var contentType))
            contentType = "application/octet-stream";

        return new FileContentResult(await File.ReadAllBytesAsync(Path.Combine(_env.WebRootPath, filePath)), contentType) { FileDownloadName = Path.GetFileName(filePath) };
    }
    public async Task Delete(string filePath)
    {
        filePath = Path.Combine(_env.WebRootPath, CleanUpPath(filePath));

        if (!File.Exists(filePath))
            throw new FileNotFoundException("File does not exist", filePath);

        //File.Delete(filePath);
        FileSystem.DeleteFile(filePath, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin);
        await Task.CompletedTask;
    }
    #endregion

    #region File Registry Manager
    public byte[] GetNewRegistryAsBytes() => Guid.NewGuid().ToByteArray(); // To generate a regestry table in db and send that as guid
    public async Task AppendRegistryToFile(string filePath)
    {
        await using FileStream fileStream = new FileStream(CleanUpPath(filePath), FileMode.Append, FileAccess.Write, FileShare.None, 4096, true);
        await fileStream.WriteAsync(GetNewRegistryAsBytes());
    }
    public async Task<IFormFile> AppendRegistryToFile(IFormFile formFile)
    {
        MemoryStream memoryStream = new();

        await formFile.CopyToAsync(memoryStream);
        await memoryStream.WriteAsync(GetNewRegistryAsBytes());
        memoryStream.Position = 0;

        return new FormFile(memoryStream, 0, memoryStream.Length, formFile.Name, formFile.FileName)
        {
            Headers = formFile.Headers,
            ContentType = formFile.ContentType
        };
    }

    public async Task<Guid> GetFileRegistry(string filePath)
    {
        filePath = Path.Combine(_env.WebRootPath, CleanUpPath(filePath));
        await using FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
        fileStream.Seek(-_registryByteSize, SeekOrigin.End);
        byte[] buffer = new byte[_registryByteSize];
        await fileStream.ReadAsync(buffer, 0, _registryByteSize);
        return new Guid(buffer);
    }
    public async Task<string?> GetFileFromRegistry(Guid registry)
    {
        string[] files = Directory.GetFiles(_env.WebRootPath, "*", System.IO.SearchOption.AllDirectories);
        int start = string.IsNullOrEmpty(_lastCheckedFile) ? 0 : (Array.IndexOf(files, _lastCheckedFile) + 1) % files.Length;

        for (int i = 0; i < files.Length; i++)
        {
            var file = files[(start + i) % files.Length];

            if (new FileInfo(file).Length < _registryByteSize) continue;

            await using FileStream fileStream = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.Read);
            fileStream.Seek(-_registryByteSize, SeekOrigin.End);
            byte[] buffer = new byte[_registryByteSize];
            await fileStream.ReadAsync(buffer, 0, _registryByteSize);

            if (buffer.SequenceEqual(registry.ToByteArray()))
                return _registryCache[registry] = _lastCheckedFile = file;

            _lastCheckedFile = file;
        }

        return null;
    }
    public async Task<string?> FindFileByName(string fileName)
    {
        foreach (string file in Directory.GetFiles(_env.WebRootPath, "*.*", System.IO.SearchOption.TopDirectoryOnly))
            if (Path.GetFileNameWithoutExtension(file).Equals(fileName, StringComparison.OrdinalIgnoreCase))
                return file;

        await Task.CompletedTask;
        return null;
    }
    public async Task<string?> SearchAndGetFile(string input)
    {
        if (Guid.TryParse(input, out var registry))
            return await GetFileFromRegistry(registry);

        string cleanedPath = CleanUpPath(input);

        if (File.Exists(cleanedPath))
            return cleanedPath;

        return await FindFileByName(cleanedPath);
    }
    private string CleanUpPath(string filePath) => Regex.Replace(filePath, @"^[a-zA-Z]:[\\/]", string.Empty).TrimStart('/', '\\');
    #endregion

    public bool IsFileCompressed(IFormFile formFile)
    {
        const int signatureLength = 6; // Enough to check most signatures
        byte[] signatureBuffer = new byte[signatureLength];
        using var stream = formFile.OpenReadStream();
        stream.Read(signatureBuffer, 0, signatureLength);

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
        filePath = CleanUpPath(filePath);
        filePath = Path.Combine(_env.WebRootPath, filePath);

        if (string.IsNullOrWhiteSpace(filePath) || !Directory.Exists(filePath))
            throw new DirectoryNotFoundException("The specified directory does not exist.");

        List<string> contentList = new();

        contentList.AddRange(Directory.GetDirectories(filePath).Select(Path.GetFileName)!);
        contentList.AddRange(Directory.GetFiles(filePath).Select(Path.GetFileName)!);

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
        filePath = CleanUpPath(filePath);
        filePath = Path.Combine(_env.WebRootPath, filePath);

        if (Directory.Exists(filePath))
            throw new Exception("Folder already exists, return false or handle as needed");

        Directory.CreateDirectory(filePath);

        await Task.CompletedTask;
        return true;
    }
}