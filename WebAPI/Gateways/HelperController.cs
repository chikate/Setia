using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Setia.Contexts.Base;
using Setia.Contexts.Gov;
using Setia.Models.Base;
using Setia.Services.Interfaces;

namespace Setia.Controllers
{
    [ApiController]
    [Authorize]
    [Route("/api/[controller]/[action]")]
    public class HelperController : ControllerBase
    {
        private readonly BaseContext _context;
        private readonly GovContext _contextGov;
        private readonly ILogger<HelperController> _logger;
        private readonly IAudit _audit;
        private readonly IAuth _auth;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public HelperController
        (
            BaseContext context,
            GovContext contextGov,
            ILogger<HelperController> logger,
            IAudit audit,
            IAuth auth,
            IWebHostEnvironment hostingEnvironment
        )
        {
            _context = context;
            _contextGov = contextGov;
            _logger = logger;
            _audit = audit;
            _auth = auth;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpPost]
        public async Task<IActionResult> Upload(IEnumerable<IFormFile> files)
        {
            try
            {
                string Author = _auth.GetCurrentUser().Username;
                if (Author.IsNullOrEmpty()) return BadRequest("Invalid author ID");

                string userDirectory = Path.Combine(_hostingEnvironment.WebRootPath, Author);
                if (!Directory.Exists(userDirectory)) Directory.CreateDirectory(userDirectory);

                foreach (IFormFile file in files)
                {
                    if (file.Length > 0)
                    {
                        using (FileStream stream = new FileStream(Path.Combine(userDirectory, file.FileName), FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                            await _audit.LogAuditTrail<FileStream>(stream);
                        }
                    }
                }

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, this.GetType().FullName);
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
                _logger.LogError(ex, this.GetType().FullName);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetUserProfile(string username)
        {
            try { return Ok(await _context.Users.FirstOrDefaultAsync(u => u.Username == username)); }
            catch (Exception ex)
            {
                _logger.LogError(ex, this.GetType().FullName);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetPostsForUser(string username)
        {
            try { return Ok(await _contextGov.Posts.Where(u => u.Author == username && !u.Tags.Any(t => t.Contains("Deleted"))).ToListAsync()); }
            catch (Exception ex)
            {
                _logger.LogError(ex, this.GetType().FullName);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> UpdateCurentUserAvatar(string avatarUrl)
        {
            try
            {
                UserModel? user = await _context.Users.FindAsync(_auth.GetCurrentUser()?.Id);
                if (user == null) throw new Exception("User Not Found");
                user.Avatar = avatarUrl;
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, this.GetType().FullName);
                return BadRequest(ex.Message);
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
                    UserId = toUser.Id,
                    Title = $"{_auth.GetCurrentUser()?.Username} wants to be your friend",
                    Author = _auth.GetCurrentUser()?.Username,
                });

                await _context.SaveChangesAsync();

                return Ok("Friend request sent");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, this.GetType().FullName);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> AcceptFriendRequest(Guid notificationId)
        {
            try
            {
                NotificationModel? notification = await _context.Notifications.Where(u => u.Id == notificationId).SingleOrDefaultAsync();
                if (notification == null) throw new Exception("Invalid notification");

                UserModel? fromUser = await _context.Users.Where(u => u.Username == notification.Author).SingleOrDefaultAsync();
                if (fromUser == null) throw new Exception("There is no user with this username");

                UserModel? currentUser = _auth.GetCurrentUser();
                fromUser.Friends.Add(currentUser.Id);
                currentUser.Friends.Add(fromUser.Id);

                _context.Users.Update(fromUser);
                _context.Users.Update(currentUser);

                return Ok($"You are now friends with {fromUser.Username}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, this.GetType().FullName);
                return BadRequest(ex.Message);
            }
        }
    }
}