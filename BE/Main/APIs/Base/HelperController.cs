using Main.Data.Contexts;
using Main.Data.Models;
using Main.Services;
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
    private readonly IAuditService _audit;
    private readonly IAuthService _auth;
    private readonly IWebHostEnvironment _hostingEnvironment;

    public HelperController
    (
        BaseContext context,
        GovContext contextGov,
        IAuditService audit,
        IAuthService auth,
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

    [HttpGet]
    public async Task<IActionResult> GetUserTags(string? specific = null, Guid? userId = null)
    {
        try { return Ok(await _auth.GetUserTags(specific, userId)); }
        catch (Exception ex) { return BadRequest(ex.Message); }
    }

    [HttpGet]
    public async Task<IActionResult> GetUserProfile(string username)
    {
        try { return Ok(await _context.Set<UserModel>().FirstOrDefaultAsync(u => u.Username == username)); }
        catch (Exception ex) { return BadRequest(ex.Message); }
    }

    [HttpGet]
    public async Task<IActionResult> GetPostsForUser(string username)
    {
        try
        {
            UserModel? user = await _context.Set<UserModel>().Where(u => u.Username == username).SingleOrDefaultAsync();
            if (user == null) throw new Exception("User Not Found");
            return Ok(await _contextGov.Set<PostModel>().Where(u => u.AuthorId == user.Id && !u.Tags.Any(t => t.Contains("Deleted"))).ToListAsync());
        }
        catch (Exception ex) { return BadRequest(ex.Message); }
    }

    [HttpGet]
    public async Task<IActionResult> GetCurentUserAvatar(string avatarUrl)
    {
        try
        {
            UserModel? user = await _context.Set<UserModel>().FindAsync((await _auth.GetCurrentUser())?.Id);
            if (user == null) throw new Exception("User Not Found");
            user.Avatar = avatarUrl;
            _context.Set<UserModel>().Update(user);
            await _context.SaveChangesAsync();
            return Ok();
        }
        catch (Exception ex) { return BadRequest(ex.Message); }
    }

    [HttpGet]
    public async Task<IActionResult> SendFriendRequest(string username)
    {
        try
        {
            UserModel? toUser = await _context.Set<UserModel>().SingleOrDefaultAsync(u => u.Username == username);
            if (toUser == null) throw new Exception("There is no user with this username");

            await _context.Set<NotificationModel>().AddAsync(new NotificationModel
            {
                ToUserId = toUser.Id,
                Title = $"{(await _auth.GetCurrentUser())?.Username} wants to be your friend",
                AuthorId = (await _auth.GetCurrentUser())?.Id,
            });

            await _context.SaveChangesAsync();

            return Ok("Friend request sent");
        }
        catch (Exception ex) { return BadRequest(ex.Message); }
    }

    [HttpGet]
    public async Task<IActionResult> AcceptFriendRequest(Guid notificationId)
    {
        try
        {
            NotificationModel? notification = await _context.Set<NotificationModel>().Where(u => u.Id == notificationId).SingleOrDefaultAsync();
            if (notification == null) throw new Exception("Invalid notification");

            UserModel? fromUser = await _context.Set<UserModel>().Where(u => u.Id == notification.AuthorId).SingleOrDefaultAsync();
            if (fromUser == null) throw new Exception("There is no user with this username");

            UserModel? currentUser = await _auth.GetCurrentUser();
            if (currentUser == null) throw new Exception("User Not Found");
            fromUser.Friends.Add(currentUser.Id);
            currentUser.Friends.Add(fromUser.Id);

            _context.Set<UserModel>().Update(fromUser);
            _context.Set<UserModel>().Update(currentUser);

            return Ok($"You are now friends with {fromUser.Username}");
        }
        catch (Exception ex) { return BadRequest(ex.Message); }
    }
}