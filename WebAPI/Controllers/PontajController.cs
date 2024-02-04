using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Setia.Data;
using Setia.Models;
using Setia.Services.Interfaces;
using System.Text.Json;

namespace Setia.Controllers
{
    [ApiController]
    [Authorize]
    [Route("/api/[controller]/[action]")]
    public class PontajController : ControllerBase
    {
        private readonly SetiaContext _context;
        private readonly ILogger<PontajController> _logger;
        private readonly IMapper _mapper;
        private readonly IAudit _audit;
        private readonly IAuth _auth;

        public PontajController
        (
            SetiaContext context,
            ILogger<PontajController> logger,
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
        public async Task<ActionResult<IEnumerable<PontajModel>>> GetAll()
        {
            try
            {
                return Ok(await _context.Pontaj
                    .Where(p => p.Deleted == false) // temporary
                    .Include(i => i.User)
                    .ToListAsync());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, this.GetType().FullName);
                return BadRequest(ex);
            }
        }

        [HttpGet]
        public ActionResult<IEnumerable<PontajModel>> GetAllWithFilter([FromQuery] PontajModel filter)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                return Ok(AddFilter(_context.Pontaj.AsQueryable(), filter));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, this.GetType().FullName);
                return BadRequest(ex);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] PontajModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                model.Id = 0;
                model.Id_CreatedBy = await _auth.GetCurrentUserId();
                model.Id_User = await _auth.GetCurrentUserId();

                await _context.Pontaj.AddAsync(_mapper.Map<PontajModel>(model));
                await _context.SaveChangesAsync();

                return Ok("Added");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, this.GetType().FullName);
                return BadRequest(ex);
            }
            finally
            {
                try
                {
                    var last_id_added = await _context.Pontaj.OrderBy(x => x.Id).LastOrDefaultAsync(); // we need a better method here
                    await _audit.Add(new AuditModel
                    {
                        Id_Executioner = await _auth.GetCurrentUserId(),
                        Entity = typeof(PontajModel).ToString(),
                        Id_Entity = last_id_added?.Id,
                        Payload = JsonSerializer.Serialize(model)
                    });
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, this.GetType().FullName);
                }
            }
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] PontajModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                model.Id_LastUpdateBy = await _auth.GetCurrentUserId();
                model.LastUpdateDate = DateTime.Now;

                _context.Pontaj.Update(_mapper.Map<PontajModel>(model));
                await _context.SaveChangesAsync();

                return Ok("Updated");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, this.GetType().FullName);
                return BadRequest(ex);
            }
            finally
            {
                await _audit.Add(new AuditModel
                {
                    Entity = typeof(PontajModel).ToString(),
                    Id_Entity = model.Id,
                    Payload = _audit.CompareObjects(model, model),
                });
                try
                {
                    await _audit.Add(new AuditModel
                    {
                        Id_Executioner = await _auth.GetCurrentUserId(),
                        Entity = typeof(PontajModel).ToString(),
                        Id_Entity = model.Id,
                        Payload = _audit.CompareObjects(model, model)
                    });
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, this.GetType().FullName);
                }
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

                var pontajToDelete = await _context.Pontaj.FindAsync(id);
                if (pontajToDelete != null)
                {
                    _context.Pontaj.Remove(pontajToDelete);
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
            finally
            {
                try
                {
                    await _audit.Add(new AuditModel
                    {
                        Id_Executioner = await _auth.GetCurrentUserId(),
                        Entity = typeof(PontajModel).ToString(),
                        Id_Entity = id,
                        Payload = "DELETED"
                    });
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, this.GetType().FullName);
                }
            }
        }

        private static IQueryable<PontajModel> AddFilter(IQueryable<PontajModel> query, PontajModel filter)
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