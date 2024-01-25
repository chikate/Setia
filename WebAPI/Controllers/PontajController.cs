using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Setia.Data;
using Setia.Models;

namespace Setia.Controllers
{
    [ApiController]
    [Route("/api/[controller]/[action]")]
    public class PontajController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<PontajModel>>> GetAll()
        {
            try
            {
                using SetiaContext context = new SetiaContext();
                var pontaj = await context.Pontaj.ToListAsync();
                if(pontaj != null)
                {
                    return Ok(pontaj);
                }
                else 
                {
                    return NotFound("Not found");
                }
            }
            catch
            {
                return Unauthorized();
            }
        }
     
        [HttpGet]
        public async Task<ActionResult<List<PontajModel>>> GetAllWithFilter([FromBody] PontajModel filter)
        {
            try
            {
                if(filter == null)
                {
                    return BadRequest("Bad filter");
                }

                using SetiaContext context = new SetiaContext();
                var pontaj = await context.Pontaj
                    .Where(p => p.Id == filter.Id)
                    .ToListAsync();
                if (pontaj != null)
                {
                    return Ok(pontaj);
                }
                else
                {
                    return NotFound("Not found");
                }
            }
            catch
            {
                return Unauthorized();
            }
        }

        [HttpPost]
        public async Task<ActionResult<bool>> Add([FromBody] PontajModel model)
        {
            try
            {
                using SetiaContext context = new SetiaContext();
                await context.Pontaj.AddAsync(new PontajModel()
                {
                    Id_User = model.Id_User,
                    BeginTime = model.BeginTime,
                    EndTime = model.EndTime,
                    Description = model.Description,
                });
                context.SaveChanges();
                return Ok("Added");
            }
            catch
            {
                return Unauthorized();
            }
        }

        [HttpPut]
        public async Task<ActionResult<bool>> Update([FromBody] PontajModel model)
        {
            try
            {
                using SetiaContext context = new SetiaContext();
                context.Pontaj.Update(model);
                context.SaveChanges();
                return Ok("Updated");
            }
            catch
            {
                return Unauthorized();
            }
        }

        [HttpDelete]
        public async Task<ActionResult<bool>> Delete([FromBody] int id)
        {
            try
            {
                using SetiaContext context = new SetiaContext();
                var pontaj = await context.Pontaj.FindAsync(id);
                if (pontaj != null)
                {
                    context.Pontaj.Remove(pontaj);
                    context.SaveChanges();
                    return Ok("Deleted");
                }
                else
                {
                    return NotFound("Not found");
                }
            }
            catch
            {
                return Unauthorized();
            }
        }

        private static IQueryable<PontajModel> AddFilter(IQueryable<PontajModel> query, PontajModel filter)
        {
            if (filter == null)
            {
                return query;
            }

            foreach (var item in filter.GetType().GetProperties()) {
                if (filter.Id > 0)
                {
                    query = query.Where(q => q.Id == filter.Id);
                }
            }

            if(filter.Id > 0)
            {
                query = query.Where(q => q.Id == filter.Id);
            }

            if(filter.User != null)
            {
                query = query.Where(q => q.User == filter.User);
            }

            return query;
        }
    }
}