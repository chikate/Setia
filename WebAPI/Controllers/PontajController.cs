using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Setia.Data;
using Setia.Models;
using Setia.Services.Interfaces;

namespace Setia.Controllers
{
    [ApiController]
    [Route("/api/[controller]/[action]")]
    public class PontajController : ControllerBase
    {
        private readonly SetiaContext _context;
        private readonly ILogger<PontajController> _logger;
        private readonly IMapper _mapper;
        private readonly IAudit _audit;

        public PontajController
        (
            SetiaContext context,
            ILogger<PontajController> logger,
            IMapper mapper,
            IAudit audit
        )
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
            _audit = audit;
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
                return Unauthorized(ex);
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PontajModel>>> GetAllWithFilter([FromQuery] PontajModel filter)
        {
            try
            {
                if (filter == null)
                {
                    return BadRequest("Bad filter");
                }
                return Ok(await AddFilter(_context.Pontaj.AsQueryable(), filter));
            }
            catch (Exception ex)
            {
                return Unauthorized(ex);
            }
        }

        [HttpPost]
        public async Task<ActionResult<bool>> Add([FromBody] PontajModel model)
        {
            try
            {
                model.Id = 0;
                model.Id_User = 6; // temporary
                model.CreationDate = DateTime.Now;
                await _context.Pontaj.AddAsync(_mapper.Map<PontajModel>(model));
                await _context.SaveChangesAsync();
                return Ok("Added");
            }
            catch (Exception ex)
            {
                return Unauthorized(ex);
            }
            finally
            {
                await _audit.Add(new AuditModel{
                    Id = 0,
                    Description = "Added new pontaj",
                    Details = ""
                });
            }
        }

        [HttpPut]
        public async Task<ActionResult<bool>> Update([FromBody] PontajModel model)
        {
            try
            {
                model.LastUpdateDate = DateTime.Now;
                _context.Pontaj.Update(_mapper.Map<PontajModel>(model));
                await _context.SaveChangesAsync();
                return Ok("Updated");
            }
            catch (Exception ex)
            {
                return Unauthorized(ex);
            }
            finally
            {
                await _audit.Add(new AuditModel{
                    Id = 0,
                    Description = model.Deleted == true ? "Pontaj deleted" : "Pontaj updated",
                    Details = "",
                    Entity = model.ToString(),
                    Id_Entity = model.Id
                });
            }
        }

        [HttpDelete]
        public async Task<ActionResult<bool>> Delete([FromBody] int id)
        {
            try
            {
                var pontajToDelete = await _context.Pontaj.FindAsync(id);
                if (pontajToDelete != null)
                {
                    _context.Pontaj.Remove(pontajToDelete);
                    await _context.SaveChangesAsync();
                    // add to audit
                    return Ok("Deleted");
                }
                else
                {
                    return NotFound("Not found");
                }
            }
            catch (Exception ex)
            {
                return Unauthorized(ex);
            }
            finally
            {
                await _audit.Add(new AuditModel{
                    Id = 0,
                    Description = "Pontaj DELETED",
                    Details = ""
                });
            }
        }

        private static async Task<IQueryable<PontajModel>> AddFilter(IQueryable<PontajModel> query, PontajModel filter)
        {
            if (filter == null || query == null)
            {
                return query;
            }

            foreach (var property in typeof(PontajModel).GetProperties())
            {
                var value = property.GetValue(filter);

                if (value != null && value.GetType() != typeof(string) && !value.Equals(Activator.CreateInstance(property.PropertyType)))
                {
                    if (property.PropertyType == typeof(string))
                    {
                        query = query.Where(item => (string)property.GetValue(item) == (string)value);
                    }
                    else
                    {
                        query = query.Where(item => property.GetValue(item).Equals(value));
                    }
                }
            }

            return query;
        }
    }
}