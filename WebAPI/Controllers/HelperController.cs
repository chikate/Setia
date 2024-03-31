using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Setia.Data;
using Setia.Services.Interfaces;

namespace Setia.Controllers
{
    [ApiController]
    [Authorize]
    [Route("/api/[controller]/[action]")]
    public class HelperController : ControllerBase
    {
        private readonly SetiaContext _context;
        private readonly ILogger<HelperController> _logger;
        private readonly IAudit _audit;
        private readonly IAuth _auth;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public HelperController
        (
            SetiaContext context,
            ILogger<HelperController> logger,
            IAudit audit,
            IAuth auth,
            IWebHostEnvironment hostingEnvironment
        )
        {
            _context = context;
            _logger = logger;
            _audit = audit;
            _auth = auth;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpPost]
        public async Task<IActionResult> Upload(List<IFormFile> files)
        {
            try
            {
                int authorId = await _auth.GetCurrentUserId();

                if (authorId <= 0) return BadRequest("Invalid author ID");

                var userDirectory = Path.Combine(_hostingEnvironment.WebRootPath, authorId.ToString());
                if (!Directory.Exists(userDirectory))
                {
                    Directory.CreateDirectory(userDirectory);
                }

                foreach (var file in files)
                {
                    if (file.Length > 0)
                    {
                        using (var stream = new FileStream(Path.Combine(userDirectory, file.FileName), FileMode.Create))
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
        public ActionResult<IEnumerable<string>> GetActions()
        {
            return Ok(_auth.GetActions());
        }
    }
}