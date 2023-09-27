using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {

        #region Cripting
        private readonly int key = 467392581;
        private string Cipher(string rawString)
        {
            if (rawString != null)
            {
                char[] cipheredString = new char[rawString.Length];
                for (int i = 0; i < rawString.Length; i++)
                {
                    char ch = rawString[i];

                    if (char.IsLetter(ch))
                    {
                        char shiftedChar = (char)(ch + key);

                        if ((char.IsLower(ch) && shiftedChar > 'z') ||
                            (char.IsUpper(ch) && shiftedChar > 'Z'))
                        {
                            shiftedChar = (char)(ch - (26 - key));
                        }

                        cipheredString[i] = shiftedChar;
                        return new string(cipheredString);
                    }
                }
            }
            return String.Empty.ToString();
        }
        private string Decipher(string cipheredString)
        {
            if (cipheredString != null)
            {
                char[] decipheredString = new char[cipheredString.Length];
                for (int i = 0; i < cipheredString.Length; i++)
                {
                    char ch = cipheredString[i];

                    if (char.IsLetter(ch))
                    {
                        char shiftedChar = (char)(ch - key);

                        if ((char.IsLower(ch) && shiftedChar > 'z') ||
                            (char.IsUpper(ch) && shiftedChar > 'Z'))
                        {
                            shiftedChar = (char)(ch - (26 + key));
                        }

                        decipheredString[i] = shiftedChar;
                        return new string(decipheredString);
                    }
                }
            }
            return "";
        }
        #endregion

        #region Register
        public enum RegisterStatus
        {
            AccountCreated,
            InvalidPassword,
            InvalidUsername,
            UsernameAlreadyTaken,
            InvalidEmail,
            RegistrationFailed,
        }

        [Route("Register")]
        [HttpPost]
        public RegisterStatus Register(string email, string username, string password)
        {
            string EmailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            if (Regex.IsMatch(email, EmailPattern))
            {
                if (username.Length > 3)
                {
                    if (password.Length > 3)
                    {
                        using SetiaContext context = new SetiaContext();
                        var user = context.Users.Where(a => a.Username == Decipher(username)).SingleOrDefault();
                        if (user == null)
                        {
                            context.Users.Add(new UserModel()
                            {
                                Username = username,
                                Password = password,
                                Email = email,
                                Name = "",
                                Coins = 0,
                                CreationDate = DateTime.Now,
                            });
                            context.SaveChanges();
                            return RegisterStatus.AccountCreated;
                        }
                        else
                        {
                            return RegisterStatus.UsernameAlreadyTaken;
                        }
                    }
                    else
                    {
                        return RegisterStatus.InvalidPassword;
                    }
                }
                else
                {
                    return RegisterStatus.InvalidUsername;
                }
            }
            else
            {
                return RegisterStatus.InvalidEmail;
            }
        }
        #endregion

        #region Login
        public enum LoginStatus
        {
            LoginSuccessful,
            InvalidPassword,
            InvalidUsername,
            LoginFailed,
        }

        public struct UserData
        {
            public string name;
            public int coins;
        }

        public struct LoginData
        {
            public LoginStatus status;
            public UserData data;
        }

        [Route("Login")]
        [HttpPost]
        public LoginData Login(string username, string password)
        {
            LoginData loginData = new LoginData();
            if (username.Length > 3)
            {
                if (password.Length > 3)
                {
                    SetiaContext context = new SetiaContext();
                    var user = context.Users.Where(a => a.Username == Decipher(username)).Single();//should check if is the only one found?
                    loginData.status = LoginStatus.LoginSuccessful;
                    loginData.data = new UserData();
                    loginData.data.name = "user.Name";
                    loginData.data.coins = 0;
                }
                else
                {
                    loginData.status = LoginStatus.InvalidPassword;
                }
            }
            else
            {
                loginData.status = LoginStatus.InvalidUsername;
            }
            return loginData;
        }
        #endregion

    }
}