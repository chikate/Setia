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
    public class RolesController : ControllerBase
    {
        private readonly SetiaContext _context;
        private readonly ILogger<RolesController> _logger;
        private readonly IAudit _audit;
        private readonly IAuth _auth;

        public RolesController
        (
            SetiaContext context,
            ILogger<RolesController> logger,
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
        public async Task<ActionResult<IEnumerable<RoleModel>>> GetAll()
        {
            try
            {
                int currentUserId = await _auth.GetCurrentUserId();
                var pontajList = await _context.Roles
                    .Where(p => p.Deleted == false)
                    .ToListAsync();
                return Ok(pontajList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, this.GetType().FullName);
                return BadRequest("An error occurred while retrieving RS records.");
            }
        }

        [HttpGet]
        public ActionResult<IEnumerable<RoleModel>> GetAllWithFilter([FromQuery] RoleModel filter)
        {
            try
            {
                var filteredRSList = AddFilter(_context.Roles.AsQueryable(), filter).ToList();
                return Ok(filteredRSList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, this.GetType().FullName);
                return BadRequest("An error occurred while retrieving Role records.");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] RoleModel model)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest("Invalid model state.");

                model.Id = 0;
                model.Id_Executioner = await _auth.GetCurrentUserId();


                await _context.Roles.AddAsync(model);
                await _context.SaveChangesAsync();

                await _audit.LogAuditTrail(model);

                return Ok("RS record added successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, this.GetType().FullName);
                return BadRequest("An error occurred while adding Role record.");
            }
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] RoleModel model)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest("Invalid model state.");

                model.Id_Executioner = await _auth.GetCurrentUserId();

                var oldModel = await _context.Roles.FindAsync(model.Id);

                _context.Roles.Update(model);
                await _context.SaveChangesAsync();

                await _audit.LogAuditTrail(model, oldModel);

                return Ok("RS record updated successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, this.GetType().FullName);
                return BadRequest("An error occurred while updating RS record.");
            }
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var pontajToDelete = await _context.Roles.FindAsync(id);

                if (pontajToDelete == null) return NotFound();

                _context.Roles.Remove(pontajToDelete);
                await _context.SaveChangesAsync();

                await _audit.LogAuditTrail(pontajToDelete);

                return Ok("RS record deleted successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, this.GetType().FullName);
                return BadRequest("An error occurred while deleting RS record.");
            }
        }

        private IQueryable<RoleModel> AddFilter(IQueryable<RoleModel> query, RoleModel filter)
        {
            if (filter != null)
            {
                foreach (var property in typeof(RoleModel).GetProperties())
                {
                    query = query.Where(item => property.GetValue(item).Equals(property.GetValue(filter)));
                }
            }
            return query;
        }
    }
}