using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Setia.Data;
using Setia.Models;
using Setia.Services.Interfaces;
using System.Text.Json;

namespace Setia.Controllers
{
    [ApiController]
    [Route("/api/[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private readonly SetiaContext _context;
        private readonly ILogger<UserController> _logger;
        private readonly IMapper _mapper;
        private readonly IAudit _audit;
        private readonly IAuth _auth;

        public UserController
        (
            SetiaContext context,
            ILogger<UserController> logger,
            IMapper mapper,
            IAudit audit,
            IAuth auth
        )
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
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
                return Unauthorized(ex);
            }
        }

        [HttpGet]
        public ActionResult<IEnumerable<UserModel>> GetAllWithFilter([FromQuery] UserModel filter)
        {
            try
            {
                return Ok(AddFilter(_context.Users.AsQueryable(), filter));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, this.GetType().FullName);
                return Unauthorized(ex);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] UserModel model)
        {
            try
            {
                model.Id = 0;
                model.Id_CreatedBy = await _auth.GetCurrentUserId();

                await _context.Users.AddAsync(_mapper.Map<UserModel>(model));
                await _context.SaveChangesAsync();

                return Ok("Added");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, this.GetType().FullName);
                return Unauthorized(ex);
            }
            finally
            {
                var last_id_added = await _context.Users.OrderBy(x => x.Id).LastOrDefaultAsync(); // we need a better method here
                await _audit.Add(new AuditModel
                {
                    Entity = typeof(UserModel).ToString(),
                    Id_Entity = last_id_added.Id,
                    Payload = JsonSerializer.Serialize(model)
                });
            }
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] UserModel model)
        {
            try
            {
                model.Id_LastUpdateBy = await _auth.GetCurrentUserId();
                model.LastUpdateDate = DateTime.Now;

                _context.Users.Update(_mapper.Map<UserModel>(model));
                await _context.SaveChangesAsync();

                return Ok("Updated");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, this.GetType().FullName);
                return Unauthorized(ex);
            }
            finally
            {
                await _audit.Add(new AuditModel
                {
                    Entity = typeof(UserModel).ToString(),
                    Id_Entity = model.Id,
                    Payload = _audit.CompareObjects(model, model),
                });
            }
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
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
                return Unauthorized(ex);
            }
            finally
            {
                await _audit.Add(new AuditModel
                {
                    Entity = typeof(UserModel).ToString(),
                    Id_Entity = id,
                    Payload = "DELETED"
                });
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