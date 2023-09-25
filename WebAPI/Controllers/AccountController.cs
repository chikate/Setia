using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Text.RegularExpressions;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private string Cipher(string rawString)
        {
            return rawString;
        }
        private string Decipher(string cipheredString)
        {
            return cipheredString;
        }

        public enum RegisterPostResponse
        {
            AccountCreated,
            InvalidPassword,
            InvalidUsername,
            InvalidEmail,
            RegistrationFailed,
        }

        [Route("Register")]
        [HttpPost]
        public RegisterPostResponse Register(string email, string username, string password)
        {
            string EmailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            if (Regex.IsMatch(email, EmailPattern))
            {
                if (username.Length > 3)
                {
                    if (password.Length > 3)
                    {
                        return RegisterPostResponse.AccountCreated;
                    }
                    else
                    {
                        return RegisterPostResponse.InvalidPassword;
                    }
                }
                else
                {
                    return RegisterPostResponse.InvalidUsername;
                }
            }
            else
            {
                return RegisterPostResponse.InvalidEmail;
            }
        }

        public enum LoginPostResponse
        {
            LoginSuccessful,
            InvalidCredentials,
            LoginFailed,
        }

        [Route("Login")]
        [HttpPost]
        public LoginPostResponse Login(string username, string password)
        {
            if (username.Length > 3 && password.Length > 3)
            {
                return LoginPostResponse.LoginSuccessful;
            }
            else
            {
                return LoginPostResponse.InvalidCredentials;
            }
        }
    }
}