using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Setia.Data;
using Setia.Models;
using Setia.Services.Interfaces;

namespace Setia.Controllers
{
    [ApiController]
    [Authorize]
    [Route("/api/[controller]/[action]")]
    public class PontajController : ControllerBase
    {
        private readonly SetiaContext _context;
        private readonly ILogger<PontajController> _logger;
        private readonly IAudit _audit;
        private readonly IAuth _auth;

        public PontajController
        (
            SetiaContext context,
            ILogger<PontajController> logger,
            IAudit audit,
            IAuth auth
        )
        {
            _context = context;
            _logger = logger;
            _audit = audit;
            _auth = auth;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PontajModel>>> GetAll()
        {
            try
            {
                int currentUserId = await _auth.GetCurrentUserId();
                var pontajList = await _context.Pontaj
                    .Where(p => p.Deleted == false && p.Id_User == currentUserId)
                    .Include(p => p.User)
                    .ToListAsync();
                return Ok(pontajList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, this.GetType().FullName);
                return BadRequest("An error occurred while retrieving Pontaj records.");
            }
        }

        [HttpGet]
        public ActionResult<IEnumerable<PontajModel>> GetAllWithFilter([FromQuery] PontajModel filter)
        {
            try
            {
                var filteredPontajList = AddFilter(_context.Pontaj.AsQueryable(), filter).ToList();
                return Ok(filteredPontajList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, this.GetType().FullName);
                return BadRequest("An error occurred while retrieving Pontaj records.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] PontajModel model)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest("Invalid model state.");

                model.Id = 0;
                model.Author_Id = await _auth.GetCurrentUserId();
                model.Id_User = await _auth.GetCurrentUserId();

                await _context.Pontaj.AddAsync(model);
                await _context.SaveChangesAsync();

                await _audit.LogAuditTrail(model);

                return Ok("Pontaj record added successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, this.GetType().FullName);
                return BadRequest("An error occurred while adding Pontaj record.");
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] PontajModel model)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest("Invalid model state.");

                model.Author_Id = await _auth.GetCurrentUserId();

                var oldModel = await _context.Pontaj.FindAsync(model.Id);

                _context.Pontaj.Update(model);
                await _context.SaveChangesAsync();

                await _audit.LogAuditTrail(model, oldModel);

                return Ok("Pontaj record updated successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, this.GetType().FullName);
                return BadRequest("An error occurred while updating Pontaj record.");
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var pontajToDelete = await _context.Pontaj.FindAsync(id);

                if (pontajToDelete == null) return NotFound();

                _context.Pontaj.Remove(pontajToDelete);
                await _context.SaveChangesAsync();

                await _audit.LogAuditTrail(pontajToDelete);

                return Ok("Pontaj record deleted successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, this.GetType().FullName);
                return BadRequest("An error occurred while deleting Pontaj record.");
            }
        }

        private IQueryable<PontajModel> AddFilter(IQueryable<PontajModel> query, PontajModel filter)
        {
            if (filter != null)
            {
                foreach (var property in typeof(PontajModel).GetProperties())
                {
                    query = query.Where(item => property.GetValue(item).Equals(property.GetValue(filter)));
                }
            }
            return query;
        }
    }
}