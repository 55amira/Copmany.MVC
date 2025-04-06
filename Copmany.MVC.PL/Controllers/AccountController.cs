using Company.MVC.DAL.Models;
using Copmany.MVC.PL.Dto;
using Copmany.MVC.PL.Helper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Copmany.MVC.PL.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager,SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        #region SingUp

        [HttpGet]
        public IActionResult SingUp()
        {
            return View();

        }
        //P@ssW0rd
        [HttpPost]
        public async Task<IActionResult> SingUp(SingUpDto model)
        {
            if (ModelState.IsValid) // Server Side Validation 
            {
                var user = new AppUser()
                {
                    UserName = model.UserName,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    IsAgree = model.IsAgree,
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction(actionName: "SignIn");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                
            }
            return View(model);
        }
        #endregion

        #region SingIn
        [HttpGet]
        public IActionResult SingIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SingIn(SingInDto model)
        {
            if (ModelState.IsValid)
            {
                var user =await _userManager.FindByEmailAsync(model.Email);
                if (user is not null)
                {
                   var flag = await _userManager.CheckPasswordAsync(user,model.Password);
                    if ( flag)
                    {
                        //SingIn
                       var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
                        if (result.Succeeded)
                        {
                            return RedirectToAction(nameof(HomeController.Index), "Home");
                        }
                        
                    }
                }
                ModelState.AddModelError("", "Invalid Login !");
            }
            return View(model);
        }

        #endregion

        #region SignOut
        public new async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(SingIn));
        }
        #endregion

        #region Forget Password
        [HttpGet]
        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendResetPasswordUrl(ForgetPasswordDto model)
        {
            if (ModelState.IsValid)
            {
              var user = await _userManager.FindByEmailAsync(model.Email);
                if (user is not null)
                {
                    var token =await _userManager.GeneratePasswordResetTokenAsync(user);
                    var url = Url.Action("ResetPassword", "Account",new{email = model.Email },Request.Scheme);
                    var email =new Email()
                    {
                        To = model.Email,
                        Subject ="Reset Password",
                        Body = url
                    };

                   var flag = EmailSetting.SendEmail(email);
                    if (flag)
                    {
                        return RedirectToAction("CheckYorInbox");
                    }
                }
            }
            ModelState.AddModelError("", "Invalid Reset Password opertion !");
            return View("ForgetPassword",model);
        }

        [HttpGet]
        public IActionResult CheckYorInbox()
        {
            return View();
        }

        #endregion

        #region Reset Pssword
        [HttpGet]
        public IActionResult ResetPassword(string email,string token)
        {
            TempData["email"] = email;
            TempData["token"] = token;  
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto model)
        {
            if (ModelState.IsValid)
            {
                var email = TempData["email"] as string;
                var token = TempData["token"] as string;
                if (email is null || token is null) return BadRequest("Invalid Operations"); var user = await _userManager.FindByEmailAsync(email);
                if (user is not null)
                {
                    var result = await _userManager.ResetPasswordAsync(user, token, model.NewPassword);
                    if (result.Succeeded)
                    {
                        return RedirectToAction(actionName: "SignIn");
                        
                    }
                }
                ModelState.AddModelError(key: "", errorMessage: "Invalid Reset Password Operation");
            }
            return View();
        }
        #endregion
    }
}

