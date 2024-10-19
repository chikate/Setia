using Main.Data.Contexts;
using Main.Data.Models;
using Main.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Main.APIs.Base;

[ApiController]
[Route("/api/[controller]/[action]")]
public class HelperController : ControllerBase
{
    #region Dependency Injection
    private readonly BaseContext _context;
    private readonly GovContext _contextGov;
    private readonly IAudit _audit;
    private readonly IAuth _auth;
    private readonly IWebHostEnvironment _hostingEnvironment;

    public HelperController
    (
        BaseContext context,
        GovContext contextGov,
        IAudit audit,
        IAuth auth,
        IWebHostEnvironment hostingEnvironment
    )
    {
        _context = context;
        _contextGov = contextGov;
        _audit = audit;
        _auth = auth;
        _hostingEnvironment = hostingEnvironment;
    }
    #endregion

    [HttpPost]
    public async Task<IActionResult> Upload(IEnumerable<IFormFile> files)
    {
        try
        {
            Guid? AuthorId = (await _auth.GetCurrentUser())?.Id;
            if (AuthorId != null)
            {
                string userDirectory = Path.Combine(_hostingEnvironment.WebRootPath, AuthorId.ToString());
                if (!Directory.Exists(userDirectory)) Directory.CreateDirectory(userDirectory);

                foreach (IFormFile file in files)
                {
                    if (file.Length > 0)
                    {
                        using (FileStream stream = new FileStream(Path.Combine(userDirectory, file.FileName), FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                            await _audit.LogAuditTrail(stream);
                        }
                    }
                }

                return Ok();
            }
            else
            {
                return BadRequest("Invalid author ID");
            }
        }
        catch (Exception ex)
        {
            return BadRequest("An error occurred while uploading files.");
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetUserTags(string? specific = null, Guid? userId = null)
    {
        try
        {
            return Ok(await _auth.GetUserTags(specific, userId));
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetUserProfile(string username)
    {
        try { return Ok(await _context.Users.FirstOrDefaultAsync(u => u.Username == username)); }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetPostsForUser(string username)
    {
        try
        {
            UserModel? user = await _context.Users.Where(u => u.Username == username).SingleOrDefaultAsync();
            return Ok(await _contextGov.Posts.Where(u => u.AuthorId == user.Id && !u.Tags.Any(t => t.Contains("Deleted"))).ToListAsync());
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetCurentUserAvatar(string avatarUrl)
    {
        try
        {
            UserModel? user = await _context.Users.FindAsync((await _auth.GetCurrentUser())?.Id);
            if (user == null) throw new Exception("User Not Found");
            user.Avatar = avatarUrl;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }

    [HttpGet]
    public async Task<IActionResult> SendFriendRequest(string username)
    {
        try
        {
            UserModel? toUser = await _context.Users.SingleOrDefaultAsync(u => u.Username == username);
            if (toUser == null) throw new Exception("There is no user with this username");

            await _context.Notifications.AddAsync(new NotificationModel
            {
                ToUserId = toUser.Id,
                Title = $"{(await _auth.GetCurrentUser())?.Username} wants to be your friend",
                AuthorId = (await _auth.GetCurrentUser())?.Id,
            });

            await _context.SaveChangesAsync();

            return Ok("Friend request sent");
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }

    [HttpGet]
    public async Task<IActionResult> AcceptFriendRequest(Guid notificationId)
    {
        try
        {
            NotificationModel? notification = await _context.Notifications.Where(u => u.Id == notificationId).SingleOrDefaultAsync();
            if (notification == null) throw new Exception("Invalid notification");

            UserModel? fromUser = await _context.Users.Where(u => u.Id == notification.AuthorId).SingleOrDefaultAsync();
            if (fromUser == null) throw new Exception("There is no user with this username");

            UserModel? currentUser = await _auth.GetCurrentUser();
            fromUser.Friends.Add(currentUser.Id);
            currentUser.Friends.Add(fromUser.Id);

            _context.Users.Update(fromUser);
            _context.Users.Update(currentUser);

            return Ok($"You are now friends with {fromUser.Username}");
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }
}