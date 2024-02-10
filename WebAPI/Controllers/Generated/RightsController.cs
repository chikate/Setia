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
    public class RightsController : ControllerBase
    {
        private readonly SetiaContext _context;
        private readonly ILogger<RightsController> _logger;
        private readonly IAudit _audit;
        private readonly IAuth _auth;

        public RightsController
        (
            SetiaContext context,
            ILogger<RightsController> logger,
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
        public async Task<ActionResult<IEnumerable<RightModel>>> GetAll()
        {
            try
            {
                int currentUserId = await _auth.GetCurrentUserId();
                var pontajList = await _context.Rights
                    .Where(p => p.Deleted == false)
                    .ToListAsync();
                return Ok(pontajList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, this.GetType().FullName);
                return BadRequest("An error occurred while retrieving Rights records.");
            }
        }

        [HttpGet]
        public ActionResult<IEnumerable<RightModel>> GetAllWithFilter([FromQuery] RightModel filter)
        {
            try
            {
                var filteredRightsList = AddFilter(_context.Rights.AsQueryable(), filter).ToList();
                return Ok(filteredRightsList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, this.GetType().FullName);
                return BadRequest("An error occurred while retrieving Rights records.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] RightModel model)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest("Invalid model state.");

                model.Id = 0;
                model.Author_Id = await _auth.GetCurrentUserId();


                await _context.Rights.AddAsync(model);
                await _context.SaveChangesAsync();

                await _audit.LogAuditTrail(model);

                return Ok("Rights record added successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, this.GetType().FullName);
                return BadRequest("An error occurred while adding Rights record.");
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] RightModel model)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest("Invalid model state.");

                model.Author_Id = await _auth.GetCurrentUserId();

                var oldModel = await _context.Rights.FindAsync(model.Id);

                _context.Rights.Update(model);
                await _context.SaveChangesAsync();

                await _audit.LogAuditTrail(model, oldModel);

                return Ok("Rights record updated successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, this.GetType().FullName);
                return BadRequest("An error occurred while updating Rights record.");
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var pontajToDelete = await _context.Rights.FindAsync(id);

                if (pontajToDelete == null) return NotFound();

                _context.Rights.Remove(pontajToDelete);
                await _context.SaveChangesAsync();

                await _audit.LogAuditTrail(pontajToDelete);

                return Ok("Rights record deleted successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, this.GetType().FullName);
                return BadRequest("An error occurred while deleting Rights record.");
            }
        }

        private IQueryable<RightModel> AddFilter(IQueryable<RightModel> query, RightModel filter)
        {
            if (filter != null)
            {
                foreach (var property in typeof(RightModel).GetProperties())
                {
                    query = query.Where(item => property.GetValue(item).Equals(property.GetValue(filter)));
                }
            }
            return query;
        }
    }
}