using Company.MVC.DAL.Models;
using Copmany.MVC.PL.Dto;
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
    }
}

