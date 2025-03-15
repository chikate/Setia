using Main.Modules.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Main.Modules.Drive;

[Route("api/[controller]/[action]")]
public class DriveController(IDriveService driveService, IAuthService auth) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Upload(IFormFile formFile, string saveToPath = "")
    {
        try
        {
            await auth.CheckUserRight();
            return Ok(await driveService.Upload(formFile, saveToPath));
        }
        catch (Exception ex) { return BadRequest(ex.Message); }
    }

    [HttpGet]
    public async Task<IActionResult> Download([FromQuery] string filePath)
    {
        try
        {
            await auth.CheckUserRight();
            return Ok(await driveService.Download(filePath));
        }
        catch (Exception ex) { return BadRequest(ex.Message); }
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(string filePath)
    {
        try
        {
            await auth.CheckUserRight();
            await driveService.Delete(filePath);
            return Ok("Deleted");
        }
        catch (Exception ex) { return BadRequest(ex.Message); }
    }

    [HttpGet]
    public async Task<IActionResult> GetFolderContent([FromQuery] string filePath = "")
    {
        try
        {
            await auth.CheckUserRight();
            return Ok(await driveService.GetFolderContent(filePath));
        }
        catch (Exception ex) { return BadRequest(ex.Message); }
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPartitions()
    {
        try
        {
            await auth.CheckUserRight();
            return Ok(await driveService.GetAllPartitions());
        }
        catch (Exception ex) { return BadRequest(ex.Message); }
    }

    [HttpGet]
    public async Task<IActionResult> GetFileRegistry([FromQuery] string filePath)
    {
        try
        {
            await auth.CheckUserRight();

            if (filePath == null)
                throw new ArgumentNullException(nameof(filePath));

            return Ok(await driveService.GetFileRegistry(filePath));
        }
        catch (Exception ex) { return BadRequest(ex.Message); }
    }

    [HttpGet]
    public async Task<IActionResult> SearchAndGetFile([FromQuery] string input)
    {
        try
        {
            await auth.CheckUserRight();
            return Ok(await driveService.SearchAndGetFile(input));
        }
        catch (Exception ex) { return BadRequest(ex.Message); }
    }
}