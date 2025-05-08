using Microsoft.AspNetCore.Mvc;

namespace Main.Modules.Drive;

[Route("api/[controller]/[action]")]
public class DriveController(IDriveService driveService) : ControllerBase
{
    [HttpGet]
    public Task Download([FromQuery] string filePath)
        => driveService.Download(filePath);

    [HttpGet] // GetFileContent
    public Task GetFolderContent([FromQuery] string filePath = "")
        => driveService.GetFolderContent(filePath);

    [HttpPost]
    public Task Upload(IFormFile formFile, string saveToPath = "")
        => driveService.Upload(formFile, saveToPath);

    [HttpDelete]
    public Task Delete(string filePath)
        => driveService.Delete(filePath);

    [HttpGet]
    public Task GetAllPartitions()
        => driveService.GetAllPartitions();

    [HttpGet]
    public Task GetFileRegistry([FromQuery] string filePath)
        => driveService.GetFileRegistry(filePath);

    [HttpGet]
    public Task SearchAndGetFile([FromQuery] string input)
        => driveService.SearchAndGetFile(input);
}