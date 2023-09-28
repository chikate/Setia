using Microsoft.AspNetCore.Mvc;
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
                if (username.Length > 1)
                {
                    if (password.Length > 1)
                    {
                        using SetiaContext context = new SetiaContext();
                        var user = context.Users.Where(a => a.Username == username).SingleOrDefault();
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

        [Route("Login")]
        [HttpPost]
        public string Login(string username, string password)
        {
            if (username.Length > 1)
            {
                if (password.Length > 1)
                {
                    try
                    {
                        SetiaContext context = new SetiaContext();
                        var user = context.Users.Where(a => a.Username == username).Single(); //should check if is the only one found?
                        return JsonSerializer.Serialize(user);
                    }
                    catch
                    {
                        return LoginStatus.LoginFailed.ToString();
                    }

                }
                else
                {
                    return LoginStatus.InvalidPassword.ToString();
                }
            }
            else
            {
                return LoginStatus.InvalidUsername.ToString();
            }
        }
        #endregion

        #region GetUserData
        [Route("GetUserData")]
        [HttpGet]
        public string GetUserData(string username, string password)
        {
            try
            {
                using SetiaContext context = new SetiaContext();
                var user = context.Users.Where(a => a.Username == username & a.Password == password).Single(); //should check if is the only one found?
                return JsonSerializer.Serialize(user);
            }
            catch
            {
                return String.Empty;
            }
        }
        #endregion

        #region Change and forgot password
        public enum SendForgetPassLinkStatus
        {
            NewPasswordMailSend,
            UserNotFound,
            FailedToSendLink,
        }

        [Route("SendForgotPassLink")]
        [HttpPost]
        public string SendForgotPassLink(string email, string username)
        {
            try
            {
                using SetiaContext context = new SetiaContext();
                var user = context.Users.Where(a => a.Email == email & a.Username == username).SingleOrDefault();
                if (user != null)
                {
                    //generate api change link
                    //send mail with new password
                    return "https://localhost:44381";
                }
                else
                {
                    return SendForgetPassLinkStatus.UserNotFound.ToString();
                }
            }
            catch
            {
                return SendForgetPassLinkStatus.FailedToSendLink.ToString();
            }
        }

        public enum ChangePasswordStatus
        {
            PasswordChanged,
            UserNotFound,
            NewPasswordInvalid,
            PasswordFailedChangeing,
        }

        [Route("ChangePassword")]
        [HttpPut]
        public ChangePasswordStatus ChangePassword(string email, string username, string currentPassword, string newPassword)
        {
            try
            {
                if (newPassword.Length > 5)
                {
                    using SetiaContext context = new SetiaContext();
                    var user = context.Users.Where(a => a.Email == email & a.Username == username & a.Password == currentPassword).SingleOrDefault();
                    if (user != null)
                    {
                        user.Password = newPassword;
                        context.SaveChanges();
                        return ChangePasswordStatus.PasswordChanged;
                    }
                    else
                    {
                        return ChangePasswordStatus.UserNotFound;
                    }
                }
                else
                {
                    return ChangePasswordStatus.NewPasswordInvalid;
                }
            }
            catch
            {
                return ChangePasswordStatus.PasswordFailedChangeing;
            }
        }
        #endregion

    }
}