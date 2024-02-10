using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Setia.Data;
using Setia.Models;
using Setia.Services.Interfaces;
using Setia.Structs;

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
        public async Task<IActionResult> Upload([FromForm] FileUploadDto files)
        {
            try
            {
                files.Author_Id = await _auth.GetCurrentUserId();

                if (files.Author_Id == null || files.Author_Id == 0) return BadRequest();

                foreach (var file in files.Files)
                {
                    if (file.Length > 0)
                    {
                        using (var stream = new FileStream(Path.Combine(_hostingEnvironment.WebRootPath, files.Author_Id.ToString(), file.FileName), FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }
                    }
                }

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, this.GetType().FullName);
                return BadRequest("An error occurred while retrieving Pontaj records.");
            }
        }
    }
}