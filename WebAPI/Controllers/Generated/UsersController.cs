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
    public class UsersController : ControllerBase
    {
        private readonly SetiaContext _context;
        private readonly ILogger<UsersController> _logger;
        private readonly IAudit _audit;
        private readonly IAuth _auth;

        public UsersController
        (
            SetiaContext context,
            ILogger<UsersController> logger,
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
        public async Task<ActionResult<IEnumerable<UserModel>>> GetAll()
        {
            try
            {
                return Ok(await _context.Users
                    .Where(p => p.Deleted == false) // temporary
                    .ToListAsync());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, this.GetType().FullName);
                return BadRequest([]);
            }
        }

        [HttpGet]
        public ActionResult<IEnumerable<UserModel>> GetAllWithFilter([FromQuery] UserModel filter)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest([]);
                }

                return Ok(AddFilter(_context.Users.AsQueryable(), filter));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, this.GetType().FullName);
                return BadRequest([]);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] UserModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                model.Id = 0;
                model.Id_Executioner = await _auth.GetCurrentUserId();


                await _context.Users.AddAsync(model);
                await _context.SaveChangesAsync();

                return Ok("Added");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, this.GetType().FullName);
                return BadRequest(ex);
            }
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] UserModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                model.Id_Executioner = await _auth.GetCurrentUserId();

                _context.Users.Update(model);
                await _context.SaveChangesAsync();

                return Ok("Updated");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, this.GetType().FullName);
                return BadRequest(ex);
            }
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                var pontajToDelete = await _context.Users.FindAsync(id);
                if (pontajToDelete != null)
                {
                    _context.Users.Remove(pontajToDelete);
                    await _context.SaveChangesAsync();
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, this.GetType().FullName);
                return BadRequest(ex);
            }

        }

        private static IQueryable<UserModel> AddFilter(IQueryable<UserModel> query, UserModel filter)
        {
            if (filter != null)
            {
                foreach (var property in typeof(UserModel).GetProperties())
                {
                    query = query.Where(item => property.GetValue(item).Equals(property.GetValue(filter)));
                }
            }
            return query;
        }
    }
}