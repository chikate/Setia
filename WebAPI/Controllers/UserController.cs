using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using WebAPI.Data;
using WebAPI.Models;
using static WebAPI.Controllers.UserController;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private string Cipher(string decipheredString)
        {
            return decipheredString;
        }
        private string Decipher(string cipheredString)
        {

            return cipheredString;
        }

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
            if (Regex.IsMatch(Decipher(email), EmailPattern))
            {
                if (username.Length > 3)
                {
                    if (password.Length > 3)
                    {
                        using SetiaContext context = new SetiaContext();
                        //var user = context.Users.Where(a => a.Username == Decipher(username)).SingleOrDefault();
                        //if(user != null)
                        //{
                            //context.Users.Add(new AccountModel()
                            //{
                            //    Username = Decipher(username),
                            //    Password = Decipher(password),
                            //    Email = Decipher(email),
                            //    CreationDate = DateTime.Now,
                            //});
                            //context.SaveChanges();
                            return RegisterStatus.AccountCreated;
                        //}
                        //else
                        //{
                        //    return RegisterStatus.UsernameAlreadyTaken;
                        //}
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
                    using SetiaContext context = new SetiaContext();
                    //var user = context.Users.Where(a => a.Username == Decipher(username)).Single();//should check if is the only one found?
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