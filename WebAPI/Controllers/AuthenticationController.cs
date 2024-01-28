using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Setia.Data;
using Setia.Models;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace Setia.Controllers
{
    [ApiController]
    [Route("/api/[controller]/[action]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly SetiaContext _context;
        private readonly ILogger<AuthenticationController> _logger;
        private readonly IMapper _mapper;

        public AuthenticationController
        (
            SetiaContext context,
            ILogger<AuthenticationController> logger,
            IMapper mapper
        )
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

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
        [HttpPost]
        public async Task<IActionResult> Register(string email, string username, string password)
        {
            try
            {
                if (email == null || !Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.(com|net|org|gov)$"))
                {
                    return Unauthorized("Invalid email");
                }

                if (username == null || username.Length < 3)
                {
                    return Unauthorized("Invalid username (too short)");
                }

                var user = await _context.Users
                    .Where(u => u.Username == username)
                    .SingleOrDefaultAsync();
                if (user != null)
                {
                    return Unauthorized("Username already exists");
                }

                if (password == null || password.Length < 6)
                {
                    return Unauthorized("Invalid password (too short)");
                }

                _context.Users.Add(new UserModel()
                {
                    Email = email,
                    Username = username,
                    Password = password,
                    CreationDate = DateTime.UtcNow,
                });
                await _context.SaveChangesAsync();

                //send email verification
                return Ok("Account created");

            }
            catch (Exception ex)
            {
                return Unauthorized(ex);
            }
        }
        #endregion

        #region Login
        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            try
            {
                if (username == null || username.Length < 3)
                {
                    return Unauthorized("Invalid username (too short)");
                }

                if (password == null || password.Length < 6)
                {
                    return Unauthorized("Invalid password (too short)");
                }

                var user = await _context.Users
                    .Where(u => u.Username == username & u.Password == password)
                    .FirstOrDefaultAsync();
                if (user != null)
                {
                    return Ok(JsonSerializer.Serialize(user));
                }
                else
                {
                    return NotFound("Invalid credentials / User not found");
                }
            }
            catch (Exception ex)
            {
                return Unauthorized(ex);
            }
        }
        #endregion

        #region GetUserData
        [HttpGet]
        public async Task<IActionResult> GetUserData(string username, string password)
        {
            try
            {
                var user = await _context.Users.Where(u => u.Username == username & u.Password == password).SingleAsync(); //should check if is the only one found?
                if (user != null)
                {
                    return Ok(JsonSerializer.Serialize(user));
                }
                else
                {
                    return NotFound("User not found");
                }
            }
            catch (Exception ex)
            {
                return Unauthorized(ex);
            }
        }
        #endregion

        #region Password Management
        [HttpPatch]
        public async Task<IActionResult> ChangePassword(string email, string username, string currentPassword, string newPassword)
        {
            try
            {
                if (email == null || !Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.(com|net|org|gov)$"))
                {
                    return Unauthorized("Invalid email");
                }

                if (username == null || username.Length < 3)
                {
                    return Unauthorized("Invalid username (too short)");
                }

                if (currentPassword == null || currentPassword.Length < 6)
                {
                    return Unauthorized("Invalid password (too short)");
                }

                if (newPassword == null || newPassword.Length < 6)
                {
                    return Unauthorized("Invalid new password (too short)");
                }

                var user = await _context.Users
                    .Where(u => u.Email == email & u.Username == username & u.Password == currentPassword)
                    .SingleOrDefaultAsync();
                if (user != null)
                {
                    user.Password = newPassword;
                    await _context.SaveChangesAsync();
                    return Ok("Password changed");
                }
                else
                {
                    return NotFound("User not found");
                }
            }
            catch (Exception ex)
            {
                return Unauthorized(ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string email, string username)
        {
            try
            {
                if (email == null || !Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.(com|net|org|gov)$"))
                {
                    return Unauthorized("Invalid email");
                }

                if (username == null || username.Length < 3)
                {
                    return Unauthorized("Invalid username (too short)");
                }

                var user = await _context.Users
                    .Where(u => u.Email == email & u.Username == username)
                    .SingleOrDefaultAsync();
                if (user != null)
                {
                    //generate api change link
                    //send mail with new password link
                    return Ok("https://localhost:44381");
                }
                else
                {
                    return NotFound("User not found");
                }
            }
            catch (Exception ex)
            {
                return Unauthorized(ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> ForgotUsername(string email)
        {
            try
            {
                if (email == null || !Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.(com|net|org|gov)$"))
                {
                    return Unauthorized("Invalid email");
                }

                List<string> usernames = await _context.Users
                    .Where(u => u.Email == email)
                    .Select(u => u.Username)
                    .ToListAsync();
                if (usernames != null)
                {
                    // send email with users
                    return Ok(usernames);
                }
                else
                {
                    return NotFound("User not found");
                }
            }
            catch (Exception ex)
            {
                return Unauthorized(ex);
            }
        }
        #endregion
    }
}