using Main.Services;
using Microsoft.AspNetCore.Mvc;

namespace Main.APIs.Base;

[ApiController]
[Route("/api/[controller]/[action]")]
public class FileManagerController : ControllerBase
{
    #region Dependency Injection
    private readonly IFileManagerService _archiveService;
    private readonly IAuthService _auth;

    public FileManagerController
    (
        IFileManagerService archiveService,
        IAuthService auth
    )
    {
        _archiveService = archiveService;
        _auth = auth;
    }
    #endregion

    [HttpPost]
    public async Task<IActionResult> Upload(IFormFile formFile, bool compressIt)
    {
        try
        {
            await _auth.CheckUserRight();
            return Ok(await _archiveService.Upload(formFile, compressIt));
        }
        catch (Exception ex) { return BadRequest(ex.Message); }
    }

    [HttpGet]
    public async Task<IActionResult> Download([FromQuery] string filePath)
    {
        try
        {
            await _auth.CheckUserRight();
            return Ok(await _archiveService.Download(filePath));
        }
        catch (Exception ex) { return BadRequest(ex.Message); }
    }

    [HttpGet]
    public async Task<IActionResult> GetFolderContent([FromQuery] string filePath = "")
    {
        try
        {
            await _auth.CheckUserRight();
            return Ok(await _archiveService.GetFolderContent(filePath));
        }
        catch (Exception ex) { return BadRequest(ex.Message); }
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPartitions()
    {
        try
        {
            await _auth.CheckUserRight();
            return Ok(await _archiveService.GetAllPartitions());
        }
        catch (Exception ex) { return BadRequest(ex.Message); }
    }

    [HttpGet]
    public async Task<IActionResult> GetRegistryNumberFromFile([FromQuery] string filePath)
    {
        try
        {
            await _auth.CheckUserRight();
            if (filePath == null) throw new ArgumentNullException(nameof(filePath));
            return Ok(await _archiveService.GetRegistryNumberFromFile(filePath));
        }
        catch (Exception ex) { return BadRequest(ex.Message); }
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(string filePath)
    {
        try
        {
            await _auth.CheckUserRight();
            await _archiveService.DeleteFile(filePath);
            return Ok("Deleted");
        }
        catch (Exception ex) { return BadRequest(ex.Message); }
    }
}