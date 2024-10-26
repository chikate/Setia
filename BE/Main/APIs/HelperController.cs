using Main.Data.Contexts;
using Main.Data.Models;
using Main.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Main.APIs;

[ApiController]
[Route("/api/[controller]/[action]")]
public class HelperController : ControllerBase
{
    #region Dependency Injection
    private readonly BaseContext _baseContext;
    private readonly GovContext _govContext;
    private readonly IAuditService _audit;
    private readonly IAuthService _auth;
    private readonly IWebHostEnvironment _hostingEnvironment;

    public HelperController
    (
        BaseContext baseContext,
        GovContext govContext,
        IAuditService audit,
        IAuthService auth,
        IWebHostEnvironment hostingEnvironment
    )
    {
        _baseContext = baseContext;
        _govContext = govContext;
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
        try { return Ok(await _baseContext.Set<UserModel>().FirstOrDefaultAsync(u => u.Username == username)); }
        catch (Exception ex) { return BadRequest(ex.Message); }
    }

    [HttpGet]
    public async Task<IActionResult> GetPostsForUser(string username)
    {
        try
        {
            UserModel? user = await _baseContext.Set<UserModel>().Where(u => u.Username == username).SingleOrDefaultAsync();
            if (user == null) throw new Exception("User Not Found");
            return Ok(await _govContext.Set<PostModel>().Where(u => u.AuthorId == user.Id && !u.Tags.Any(t => t.Contains("Deleted"))).ToListAsync());
        }
        catch (Exception ex) { return BadRequest(ex.Message); }
    }

    [HttpGet]
    public async Task<IActionResult> GetCurentUserAvatar(string avatarUrl)
    {
        try
        {
            UserModel? user = await _baseContext.Set<UserModel>().FindAsync((await _auth.GetCurrentUser())?.Id);
            if (user == null) throw new Exception("User Not Found");
            user.Avatar = avatarUrl;
            _baseContext.Set<UserModel>().Update(user);
            await _baseContext.SaveChangesAsync();
            return Ok();
        }
        catch (Exception ex) { return BadRequest(ex.Message); }
    }

    [HttpGet]
    public async Task<IActionResult> SendFriendRequest(string username)
    {
        try
        {
            UserModel? toUser = await _baseContext.Set<UserModel>().SingleOrDefaultAsync(u => u.Username == username);
            if (toUser == null) throw new Exception("There is no user with this username");

            await _baseContext.Set<NotificationModel>().AddAsync(new NotificationModel
            {
                ToUserId = toUser.Id,
                Title = $"{(await _auth.GetCurrentUser())?.Username} wants to be your friend",
                AuthorId = (await _auth.GetCurrentUser())?.Id,
            });

            await _baseContext.SaveChangesAsync();

            return Ok("Friend request sent");
        }
        catch (Exception ex) { return BadRequest(ex.Message); }
    }

    [HttpGet]
    public async Task<IActionResult> AcceptFriendRequest(Guid notificationId)
    {
        try
        {
            NotificationModel? notification = await _baseContext.Set<NotificationModel>().Where(u => u.Id == notificationId).SingleOrDefaultAsync();
            if (notification == null) throw new Exception("Invalid notification");

            UserModel? fromUser = await _baseContext.Set<UserModel>().Where(u => u.Id == notification.AuthorId).SingleOrDefaultAsync();
            if (fromUser == null) throw new Exception("There is no user with this username");

            UserModel? currentUser = await _auth.GetCurrentUser();
            if (currentUser == null) throw new Exception("User Not Found");
            fromUser.Friends.Add(currentUser.Id);
            currentUser.Friends.Add(fromUser.Id);

            _baseContext.Set<UserModel>().Update(fromUser);
            _baseContext.Set<UserModel>().Update(currentUser);

            return Ok($"You are now friends with {fromUser.Username}");
        }
        catch (Exception ex) { return BadRequest(ex.Message); }
    }
}