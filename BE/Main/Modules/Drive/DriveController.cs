using Main.Modules.Auth;
using Main.Standards;
using Microsoft.AspNetCore.Mvc;

namespace Main.Modules.Drive;

public class DriveController : StandardAPI
{
    #region Dependency Injection
    private readonly IDriveService _archiveService;
    private readonly IAuthService _auth;

    public DriveController
    (
        IDriveService archiveService,
        IAuthService auth
    )
    {
        _archiveService = archiveService;
        _auth = auth;
    }
    #endregion

    [HttpPost]
    public async Task<IActionResult> Upload(IFormFile formFile, string saveToPath = "")
    {
        try
        {
            await _auth.CheckUserRight();
            return Ok(await _archiveService.Upload(formFile, saveToPath));
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

    [HttpDelete]
    public async Task<IActionResult> Delete(string filePath)
    {
        try
        {
            await _auth.CheckUserRight();
            await _archiveService.Delete(filePath);
            return Ok("Deleted");
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
    public async Task<IActionResult> GetFileRegistry([FromQuery] string filePath)
    {
        try
        {
            await _auth.CheckUserRight();

            if (filePath == null)
                throw new ArgumentNullException(nameof(filePath));

            return Ok(await _archiveService.GetFileRegistry(filePath));
        }
        catch (Exception ex) { return BadRequest(ex.Message); }
    }

    [HttpGet]
    public async Task<IActionResult> SearchAndGetFile([FromQuery] string input)
    {
        try
        {
            await _auth.CheckUserRight();
            return Ok(await _archiveService.SearchAndGetFile(input));
        }
        catch (Exception ex) { return BadRequest(ex.Message); }
    }
}