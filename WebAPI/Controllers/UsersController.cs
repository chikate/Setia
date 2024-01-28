using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Setia.Data;
using Setia.Models;

namespace Setia.Controllers
{
    [ApiController]
    [Route("/api/[controller]/[action]")]
    public class UsersController : ControllerBase
    {
        private readonly SetiaContext _context;
        private readonly ILogger<UsersController> _logger;
        private readonly IMapper _mapper;

        public UsersController
        (
            SetiaContext context,
            ILogger<UsersController> logger,
            IMapper mapper
        )
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<bool>> Add([FromBody] UserModel model)
        {
            try
            {
                model.Id = 0;
                model.CreationDate = DateTime.Now;
                await _context.Users.AddAsync(_mapper.Map<UserModel>(model));
                await _context.SaveChangesAsync();
                // add to audit
                return Ok("Added");
            }
            catch (Exception ex)
            {
                return Unauthorized(ex);
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserModel>>> GetAll()
        {
            try
            {
                return Ok(await _context.Users
                    .Where(u => u.Deleted == false) // temporary
                    .ToListAsync());
            }
            catch (Exception ex)
            {
                return Unauthorized(ex);
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserModel>>> GetAllWithFilter([FromQuery] UserModel filter)
        {
            try
            {
                if (filter == null)
                {
                    return BadRequest("Bad filter");
                }

                var query = _context.Users.AsQueryable();
                var result = await AddFilter(query, filter);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Unauthorized(ex);
            }
        }

        [HttpPut]
        public async Task<ActionResult<bool>> Update([FromBody] UserModel model)
        {
            try
            {
                model.LastUpdateDate = DateTime.Now;
                _context.Users.Update(_mapper.Map<UserModel>(model));
                await _context.SaveChangesAsync();
                // add to audit
                return Ok("Updated");
            }
            catch (Exception ex)
            {
                return Unauthorized(ex);
            }
        }

        [HttpDelete]
        public async Task<ActionResult<bool>> Delete([FromBody] int id)
        {
            try
            {
                var userToDelete = await _context.Users.FindAsync(id);
                if (userToDelete != null)
                {
                    _context.Users.Remove(userToDelete);
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
        }

        private static async Task<IQueryable<UserModel>> AddFilter(IQueryable<UserModel> query, UserModel filter)
        {
            if (filter == null || query == null)
            {
                return query;
            }

            foreach (var property in typeof(UserModel).GetProperties())
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