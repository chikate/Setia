using Main.Modules.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.VisualBasic.FileIO;
using System.Text.RegularExpressions;

namespace Main.Modules.Drive;

public interface IDriveService
{
    Task<IFormFile> AppendRegistryToFile(IFormFile formFile);
    Task AppendRegistryToFile(string filePath);
    Task<bool> CreateFolder(string filePath);
    Task<bool> Delete(string filePath);
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

public class DriveService(IWebHostEnvironment env, IAuthService auth) : IDriveService
{
    private readonly Dictionary<Guid, string> _registryCache = new Dictionary<Guid, string>();
    private string _lastCheckedFile = "";
    private const int _registryByteSize = 16; // Size of a GUID in bytes

    /// <summary>
    /// Uploads a file to the server. It checks if the file is null or empty, creates the directory if it doesn't exist, and saves the file to the specified path. It also appends a registry GUID to the end of the file.
    /// </summary>
    /// <param name="formFile"></param>
    /// <param name="saveToPath"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public async Task<string> Upload(IFormFile formFile, string? saveToPath)
    {
        if (formFile == null || formFile.Length == 0)
            throw new ArgumentNullException(nameof(formFile), "Form file cannot be null or empty.");

        saveToPath = string.IsNullOrEmpty(saveToPath) ? (await auth.GetCurrentUser())?.Id.ToString() : CleanUpPath(saveToPath);

        // Ensure directory exists
        Directory.CreateDirectory(Path.Combine(env.WebRootPath, saveToPath!));

        saveToPath = Path.Combine(env.WebRootPath, Path.Combine(saveToPath!, formFile.FileName));

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

    /// <summary>
    /// Downloads a file from the server. It checks if the file exists, and if it does, it reads its content and returns it as a FileContentResult with the appropriate content type.
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns></returns>
    public async Task<FileContentResult> Download(string filePath)
    {
        filePath = CleanUpPath(filePath);

        if (!new FileExtensionContentTypeProvider().TryGetContentType(filePath, out var contentType))
            contentType = "application/octet-stream";

        return new FileContentResult(await File.ReadAllBytesAsync(Path.Combine(env.WebRootPath, filePath)), contentType) { FileDownloadName = Path.GetFileName(filePath) };
    }

    /// <summary>
    /// Deletes a file from the server. It checks if the file exists, and if it does, it deletes it using the FileSystem class to send it to the recycle bin.
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns></returns>
    /// <exception cref="FileNotFoundException"></exception>
    public async Task<bool> Delete(string filePath)
    {
        filePath = Path.Combine(env.WebRootPath, CleanUpPath(filePath));

        if (!File.Exists(filePath))
            throw new FileNotFoundException("File does not exist", filePath);

        //File.Delete(filePath);
        FileSystem.DeleteFile(filePath, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin);
        await Task.CompletedTask;

        return true;
    }

    /// <summary>
    /// Generates a new registry GUID as a byte array. This is used to create a registry table in the database and send that as a GUID.
    /// </summary>
    /// <returns></returns>
    public byte[] GetNewRegistryAsBytes() => Guid.NewGuid().ToByteArray(); // To generate a regestry table in db and send that as guid

    /// <summary>
    /// Appends a registry GUID to the end of the file. It opens the file in append mode and writes the registry bytes to it.
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns></returns>
    public async Task AppendRegistryToFile(string filePath)
    {
        await using FileStream fileStream = new FileStream(CleanUpPath(filePath), FileMode.Append, FileAccess.Write, FileShare.None, 4096, true);
        await fileStream.WriteAsync(GetNewRegistryAsBytes());
    }

    /// <summary>
    /// Appends a registry GUID to the end of the file. It creates a memory stream, copies the original file content to it, appends the registry bytes, and returns a new IFormFile with the updated content.
    /// </summary>
    /// <param name="formFile"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Gets the registry GUID from the last bytes of the file. It opens the file in read mode, seeks to the end minus the size of a GUID, and reads the last bytes as a GUID.
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns></returns>
    public async Task<Guid> GetFileRegistry(string filePath)
    {
        filePath = Path.Combine(env.WebRootPath, CleanUpPath(filePath));
        await using FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
        fileStream.Seek(-_registryByteSize, SeekOrigin.End);
        byte[] buffer = new byte[_registryByteSize];
        await fileStream.ReadExactlyAsync(buffer, 0, _registryByteSize);
        return new Guid(buffer);
    }

    /// <summary>
    /// Gets the file associated with the given registry GUID. It searches through files in the web root path, checking their size and comparing the last bytes to the registry GUID.
    /// </summary>
    /// <param name="registry"></param>
    /// <returns></returns>
    public async Task<string?> GetFileFromRegistry(Guid registry)
    {
        string[] files = Directory.GetFiles(env.WebRootPath, "*", System.IO.SearchOption.AllDirectories);
        int start = string.IsNullOrEmpty(_lastCheckedFile) ? 0 : (Array.IndexOf(files, _lastCheckedFile) + 1) % files.Length;

        for (int i = 0; i < files.Length; i++)
        {
            var file = files[(start + i) % files.Length];

            if (new FileInfo(file).Length < _registryByteSize) continue;

            await using FileStream fileStream = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.Read);
            fileStream.Seek(-_registryByteSize, SeekOrigin.End);
            byte[] buffer = new byte[_registryByteSize];
            await fileStream.ReadExactlyAsync(buffer, 0, _registryByteSize);

            if (buffer.SequenceEqual(registry.ToByteArray()))
                return _registryCache[registry] = _lastCheckedFile = file;

            _lastCheckedFile = file;
        }

        return null;
    }

    /// <summary>
    /// Finds a file by its name in the web root path.
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public async Task<string?> FindFileByName(string fileName)
    {
        foreach (string file in Directory.GetFiles(env.WebRootPath, "*.*", System.IO.SearchOption.TopDirectoryOnly))
            if (Path.GetFileNameWithoutExtension(file).Equals(fileName, StringComparison.OrdinalIgnoreCase))
                return file;

        await Task.CompletedTask;
        return null;
    }

    /// <summary>
    /// Searches for a file by name or registry GUID. If the input is a valid GUID, it retrieves the file associated with that GUID. Otherwise, it searches for the file by name.
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task<string?> SearchAndGetFile(string input)
    {
        if (Guid.TryParse(input, out var registry))
            return await GetFileFromRegistry(registry);

        string cleanedPath = CleanUpPath(input);

        if (File.Exists(cleanedPath))
            return cleanedPath;

        return await FindFileByName(cleanedPath);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns></returns>
    private string CleanUpPath(string filePath) => Regex.Replace(filePath, @"^[a-zA-Z]:[\\/]", string.Empty).TrimStart('/', '\\');

    /// <summary>
    /// Gets the file size in bytes.
    /// </summary>
    /// <param name="formFile"></param>
    /// <returns></returns>
    public bool IsFileCompressed(IFormFile formFile)
    {
        const int signatureLength = 6; // Enough to check most signatures
        byte[] signatureBuffer = new byte[signatureLength];
        using var stream = formFile.OpenReadStream();
        stream.ReadExactlyAsync(signatureBuffer, 0, signatureLength);

        return  // Additional checks can be added for TAR if needed
                // Check for ZIP signature "PK\x03\x04"
                signatureBuffer[0] == 0x50 && signatureBuffer[1] == 0x4B && signatureBuffer[2] == 0x03 && signatureBuffer[3] == 0x04
                // Check for RAR signature "Rar!"
                || signatureBuffer[0] == 0x52 && signatureBuffer[1] == 0x61 && signatureBuffer[2] == 0x72 && signatureBuffer[3] == 0x21
                // Check for GZ signature "\x1F\x8B"
                || signatureBuffer[0] == 0x1F && signatureBuffer[1] == 0x8B
                // Check for 7Z signature "7z"
                || signatureBuffer[0] == 0x37 && signatureBuffer[1] == 0x7A && signatureBuffer[2] == 0xBC && signatureBuffer[3] == 0xAF;
    }

    /// <summary>
    /// Gets the content of a folder.
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns></returns>
    /// <exception cref="DirectoryNotFoundException"></exception>
    public async Task<List<string>> GetFolderContent(string filePath)
    {
        filePath = CleanUpPath(filePath);
        filePath = Path.Combine(env.WebRootPath, filePath);

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

    /// <summary>
    /// Gets all partitions on the system.
    /// </summary>
    /// <returns></returns>
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

    /// <summary>
    /// Creates a folder at the specified path.
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<bool> CreateFolder(string filePath)
    {
        filePath = CleanUpPath(filePath);
        filePath = Path.Combine(env.WebRootPath, filePath);

        if (Directory.Exists(filePath))
            throw new Exception("Folder already exists, return false or handle as needed");

        Directory.CreateDirectory(filePath);

        await Task.CompletedTask;
        return true;
    }
}