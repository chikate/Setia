using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("/api/[controller]/[action]")]
    public class CRUDController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                using SetiaContext context = new SetiaContext();
                var users = await context.Users.ToListAsync();
                if(users != null)
                {
                    return Ok(JsonSerializer.Serialize(users));
                }
                else 
                {
                    return NotFound("User Not Found");
                }
            }
            catch
            {
                return Unauthorized();
            }
        }
     
        [HttpGet]
        public async Task<IActionResult> GetAllWithFilter(UserModel filter)
        {
            try
            {
                using SetiaContext context = new SetiaContext();
                var users = await context.Users
                    .Where(u => u.Id == filter.Id)
                    .Where(u => u.Username == filter.Username)
                    .Where(u => u.Password == filter.Password)
                    .ToListAsync();
                if (users != null)
                {
                    return Ok(JsonSerializer.Serialize(users));
                }
                else
                {
                    return NotFound("User Not Found");
                }
            }
            catch
            {
                return Unauthorized();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Add(UserModel model)
        {
            try
            {
                using SetiaContext context = new SetiaContext();
                context.Users.Add(new UserModel()
                {
                    Email = model.Email,
                    Username = model.Username,
                    Password = model.Password,
                    Name = model.Name,
                    CreationDate = DateTime.UtcNow,
                });
                context.SaveChanges();
                return Ok("Added");
            }
            catch
            {
                return Unauthorized();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Update(UserModel model)
        {
            try
            {
                using SetiaContext context = new SetiaContext();
                context.Users.Update(new UserModel()
                {
                    Email = model.Email,
                    Username = model.Username,
                    Password = model.Password,
                    Name = model.Name,
                    CreationDate = DateTime.UtcNow,
                });
                context.SaveChanges();
                return Ok("Updated");
            }
            catch
            {
                return Unauthorized();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                using SetiaContext context = new SetiaContext();
                var user = await context.Users.Where(u => u.Id == id).SingleOrDefaultAsync();
                if (user != null)
                {
                    context.Users.Remove(user);
                    context.SaveChanges();
                    return Ok("Deleted");
                }
                else
                {
                    return NotFound("User Not Found");
                }
            }
            catch
            {
                return Unauthorized();
            }
        }
    }
}