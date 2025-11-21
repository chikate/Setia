using Modules.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.VisualBasic.FileIO;
using System.Text.RegularExpressions;

namespace Modules.Drive;

public interface IDriveService
{
    Task<string> Upload(IFormFile formFile, string? saveToPath);
    Task<FileContentResult> Download(string filePath);
    Task<string?> Search(string input);
    Task<bool> Delete(string filePath);
    Task<bool> CreateFolder(string filePath);
    Task<List<string>> GetFolderContent(string filePath);
    Task<List<DriveInfoDTO>> GetAllPartitions();
}

public class DriveService(IWebHostEnvironment env, IAuthService auth) : IDriveService
{
    public async Task<string> Upload(IFormFile formFile, string? saveToPath)
    {
        saveToPath = Path.Combine(env.WebRootPath, NormalizePath(saveToPath) ?? (await auth.GetCurrentUser())?.Id.ToString() ?? "default", formFile.FileName);
        await formFile.CopyToAsync(new FileStream(saveToPath, FileMode.Create));
        return saveToPath;
    }

    public async Task<FileContentResult> Download(string filePath)
    {
        filePath = Path.Combine(env.WebRootPath, NormalizePath(filePath));
        if (!File.Exists(filePath)) throw new FileNotFoundException();
        new FileExtensionContentTypeProvider().TryGetContentType(filePath, out var contentType);
        return new FileContentResult(await File.ReadAllBytesAsync(filePath), contentType ?? "application/octet-stream") { FileDownloadName = Path.GetFileName(filePath) };
    }

    public Task<bool> Delete(string filePath)
    {
        filePath = Path.Combine(env.WebRootPath, NormalizePath(filePath));
        if (!File.Exists(filePath)) throw new FileNotFoundException("File does not exist", filePath);
        FileSystem.DeleteFile(filePath, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin);
        return Task.FromResult(true);
    }

    public Task<string?> Search(string input)
    {
        var cleanedPath = NormalizePath(input);
        return Task.FromResult(File.Exists(cleanedPath) ? cleanedPath : Directory.GetFiles(env.WebRootPath, "*.*", System.IO.SearchOption.TopDirectoryOnly).FirstOrDefault(f => Path.GetFileNameWithoutExtension(f).Equals(cleanedPath, StringComparison.OrdinalIgnoreCase)));
    }

    private string NormalizePath(string filePath) => Regex.Replace(filePath, @"^[a-zA-Z]:[\\/]", string.Empty).TrimStart('/', '\\');

    public Task<List<string>> GetFolderContent(string filePath)
    {
        filePath = Path.Combine(env.WebRootPath, NormalizePath(filePath));
        if (!Directory.Exists(filePath)) throw new DirectoryNotFoundException("The specified directory does not exist.");
        return Task.FromResult(Directory.EnumerateFileSystemEntries(filePath).Select(Path.GetFileName)!.Where(name => !string.IsNullOrEmpty(name)).ToList())!;
    }

    public Task<List<DriveInfoDTO>> GetAllPartitions()
        => Task.FromResult(DriveInfo.GetDrives().Where(drive => drive.IsReady).Select(d => new DriveInfoDTO
        {
            Name = d.Name,
            VolumeLabel = d.VolumeLabel,
            DriveType = d.DriveType.ToString(),
            AvailableFreeSpace = d.AvailableFreeSpace,
            TotalSize = d.TotalSize
        }).ToList());

    public Task<bool> CreateFolder(string filePath)
    {
        filePath = Path.Combine(env.WebRootPath, NormalizePath(filePath));
        if (Directory.Exists(filePath)) throw new Exception("Folder already exists");
        Directory.CreateDirectory(filePath);
        return Task.FromResult(true);
    }
}