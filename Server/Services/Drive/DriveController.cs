using Microsoft.AspNetCore.Mvc;

namespace Main.Modules.Drive;

[ApiController]
[Route("api/[controller]/[action]")]
public class DriveController(IDriveService driveService, IWebHostEnvironment env) : ControllerBase
{
    [HttpGet]
    async public Task<FileContentResult> Download([FromQuery] string filePath)
        => await driveService.Download(filePath);

    [HttpGet]
    async public Task<List<string>> GetFolderContent([FromQuery] string filePath = "")
        => await driveService.GetFolderContent(filePath);

    [HttpPost]
    async public Task<string> Upload([FromForm] IFormFile formFile, string saveToPath = "")
        => await driveService.Upload(formFile, saveToPath);

    [HttpDelete]
    async public Task<bool> Delete(string filePath)
        => await driveService.Delete(filePath);

    [HttpGet]
    async public Task<List<DriveInfoDTO>> GetAllPartitions()
        => await driveService.GetAllPartitions();

    [HttpGet]
    async public Task<string?> Search([FromQuery] string input)
        => await driveService.Search(input);

    [HttpGet]
    public string GetBasePath() => env.WebRootPath;
}