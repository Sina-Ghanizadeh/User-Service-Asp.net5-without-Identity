using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using UserService.Data.Repositories.SendMail;
using UserService.Data.Repositories.Users;
using UserService.Models;
using UserService.Models.ViewModels;
using UserService.Utility;

namespace UserService.Controllers
{
    public class AccountController : Controller
    {
        private IUserRepository _userRepository;
        private readonly IMailService _mailService;
        public AccountController(IUserRepository userRepository, IMailService mailService)
        {
            _userRepository = userRepository;
            _mailService = mailService;
        }

        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Register(RegisterViewModel register)
        {

            if (!ModelState.IsValid)
            {
                return View(register);

            }
            if (_userRepository.IsExistByEmail(register.Email.ToLower()))
            {
                ModelState.AddModelError("Email", "یک حساب کاربری با ایمیل موردنظر موجود است.");
                return View(register);
            }
            if (_userRepository.IsExistByPhone(register.PhoneNumber))
            {
                ModelState.AddModelError("PhoneNumber", "یک حساب کاربری با شماره تلفن موردنظر موجود است.");
                return View(register);
            }


            var passwordHasher = new PasswordHasher();
            var hashedPassword = passwordHasher.HashPassword(register.Password);

            User user = new User()
            {


                Email = register.Email.ToLower(),
                Password = hashedPassword,
                RegisterDate = DateTime.Now,
                PhoneNumber = register.PhoneNumber,
                FirstName = register.FirstName,
                LastName = register.LastName

            };
            _userRepository.AddUser(user);

            return View("SuccessRegister", register);
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }


            var user = _userRepository.GetUserForLogin(model.Email.ToLower(), model.Password);
            if (user == null)
            {
                ModelState.AddModelError("Email", "اطلاعات صحیح نیست");
                return View(model);
            }


            var claims = new List<Claim> {

                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Name,user.Email),
                
                

            };

            var identify = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var principal = new ClaimsPrincipal(identify);
            
            await HttpContext.SignInAsync(principal);


            return RedirectToAction(controllerName: "Home", actionName: "Index");
        }


        public IActionResult logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/Account/login");
        }

        [Route("ForgotPassword")]
        public IActionResult ForgotPassword()
        {

            return View();
        }
        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }


                if (_userRepository.IsExistByEmail(model.Email)==false)
                {
                    ModelState.AddModelError("Email", "اطلاعات صحیح نیست");
                    return View(model);
                }

                Random random = new Random();
                int code = random.Next(10000, 90000);

                
                MailRequest mail = new MailRequest()
                {

                    ToEmail = model.Email,
                    Subject = "Reset Password",
                    Body = "You Forget Your Password ." + "\n" + $"Your Verification Code Is {code}" 


                };


                await _mailService.SendEmailAsync(mail);

                ResetPasswordViewModel reset = new ResetPasswordViewModel() {

                    Email = model.Email,
                    GenCode = code.ToString()
                };

                return RedirectToAction("ResetPassword",reset);
            }
            catch (Exception)
            {

                throw;
            }

        }
        
        public IActionResult ResetPassword(ResetPasswordViewModel resetModel) {
            return View();
        }
        
        public IActionResult Reset(ResetPasswordViewModel reset) {

            if (!ModelState.IsValid)
            {
                return View("ResetPassword", reset);
            }
            if (reset.GenCode!=reset.EnterCode)
            {
                ModelState.AddModelError("EnterCode", "Verification Code Is Not Valid!");
                return View(reset);
            }

            _userRepository.ResetPassword(reset.Email, reset.Password);
           
            return View("Login");
        }
        

    }
}
