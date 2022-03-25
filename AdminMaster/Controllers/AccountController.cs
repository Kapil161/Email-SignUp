using AdminMaster.Repository.Interface;
using AdminMaster.Utils.Enums;
using AdminMaster.ViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AdminMaster.Controllers
{
    public class AccountController : Controller
    {
        private IUsers UserServices;
        public AccountController(IUsers users)
        {
            UserServices = users;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult >Login(SignInModel model)
        {
            if (ModelState.IsValid)
            {
                var result = UserServices.SignIn(model);
                if(result==SignInEnum.Success)
                {
                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Name, model.Email),

                    };
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties()
                    {
                        IsPersistent = model.RememberMe
                    });
                    return RedirectToAction("Index", "Home");
                }
                else if(result==SignInEnum.WrongCredentials)
                {
                    ModelState.AddModelError(string.Empty, "Invalid login Credential..!");
                }
                else if(result==SignInEnum.NotVerified)
                {
                    ModelState.AddModelError(string.Empty, "User Not Verified please verify first..!");
                }
                else if(result==SignInEnum.InActive)
                {
                    ModelState.AddModelError(string.Empty, "Your Account is InActive!");
                }
            }
            return View();
        }
        public async Task<IActionResult> SignOut()
        {
           await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(SignUpModel model)
        {
            if(ModelState.IsValid)
            {
                var result = UserServices.SignUp(model);
                if (result == SignUpEnum.Success)
                {
                    return RedirectToAction("VerifyAccount");
                }
                else if (result == SignUpEnum.EmailExist)
                {
                    ModelState.AddModelError(String.Empty, "Email Already Exist,Please try Another Email..!");
                }
                else
                {
                    ModelState.AddModelError(String.Empty, "Something went Wrong..!");
                }
            }
            return View(model);
        }
        public IActionResult VerifyAccount()
        {
            return View();
        }
        [HttpPost]
        public IActionResult VerifyAccount(string Otp)
        {
            if (Otp != null)
            {
                if(UserServices.VerifyAccount(Otp))
                {
                    return RedirectToAction("Login");
                }
                else 
                {
                    ModelState.AddModelError(string.Empty, "Please Enter Correct OTP..!");
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Please enter OTP..!");
            }
            return View();
        }
    }
}
