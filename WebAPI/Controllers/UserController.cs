using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {

        #region Encription
        public class Caesar
        {
            private static readonly int ShiftKey = 2758;

            public static string Cipher(string message)
            {
                StringBuilder encryptedMessage = new StringBuilder();

                foreach (char character in message)
                {
                    char encryptedChar = character;

                    if (char.IsLetter(character))
                    {
                        char baseLetter = char.IsUpper(character) ? 'A' : 'a';
                        encryptedChar = (char)(((character + ShiftKey - baseLetter) % 26) + baseLetter);
                    }

                    encryptedMessage.Append(encryptedChar);
                }

                return encryptedMessage.ToString();
            }

            public static string Decipher(string encryptedMessage)
            {
                StringBuilder decryptedMessage = new StringBuilder();

                foreach (char character in encryptedMessage)
                {
                    char decryptedChar = character;

                    if (char.IsLetter(character))
                    {
                        char baseLetter = char.IsUpper(character) ? 'A' : 'a';
                        decryptedChar = (char)(((character - ShiftKey - baseLetter + 26) % 26) + baseLetter);
                    }

                    decryptedMessage.Append(decryptedChar);
                }

                return decryptedMessage.ToString();
            }
        }
        #endregion

        #region Register
        [HttpPost("Register")]
        public async Task<ActionResult<string>> Register(string email, string username, string password)
        {
            try
            {
                using SetiaContext context = new SetiaContext();
                var user = await context.Users.Where(a => a.Username == username).SingleOrDefaultAsync();
                if (user == null)
                {
                    context.Users.Add(new UserModel()
                    {
                        Email = email,
                        Username = username,
                        Password = password,
                        Name = "",
                        Coins = "0",
                        CreationDate = DateTime.Now,
                    });
                    context.SaveChanges();
                    return Ok("Account Created");
                }
                else
                {
                    return Unauthorized("Username Already Exists");
                }
            }
            catch
            {
                return BadRequest("Registration Failed");
            }
        }
        #endregion

        #region Login
        [HttpPost("Login")]
        public async Task<ActionResult<string>> Login(string username, string password)
        {
            try
            {
                using SetiaContext context = new SetiaContext();
                var user = await context.Users.Where(a => a.Username == username & a.Password == password).FirstOrDefaultAsync();
                if (user != null)
                {
                    return Ok(JsonSerializer.Serialize(user));
                }
                else
                {
                    return NotFound("Invalid Credentials / User Not Found");
                }
            }
            catch
            {
                return BadRequest("Login Failed");
            }
        }
        #endregion

        #region GetUserData
        [HttpGet("GetUserData")]
        public async Task<ActionResult<string>> GetUserData(string username, string password)
        {
            try
            {
                using SetiaContext context = new SetiaContext();
                var user = await context.Users.Where(a => a.Username == username & a.Password == password).SingleAsync(); //should check if is the only one found?
                if(user != null)
                {
                    return Ok(JsonSerializer.Serialize(user));
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
        #endregion

        #region Password Management
        [HttpPut("ChangePassword")]
        public async Task<ActionResult<string>> ChangePassword(string email, string username, string currentPassword, string newPassword)
        {
            try
            {
                using SetiaContext context = new SetiaContext();
                var user = await context.Users.Where(a => a.Email == email & a.Username == username & a.Password == currentPassword).SingleOrDefaultAsync();
                if (user != null)
                {
                    user.Password = newPassword;
                    context.SaveChanges();
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost("SendForgotPassLink")]
        public async Task<ActionResult<string>> SendForgotPassLink(string email, string username)
        {
            try
            {
                using SetiaContext context = new SetiaContext();
                var user = await context.Users.Where(a => a.Email == email & a.Username == username).SingleOrDefaultAsync();
                if (user != null)
                {
                    //generate api change link
                    //send mail with new password
                    return Ok("https://localhost:44381");
                }
                else
                {
                    return NotFound("User Not Found");
                }
            }
            catch
            {
                return BadRequest("Failed To Send Link");
            }
        }
        #endregion

    }
}