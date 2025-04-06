using Company.MVC.DAL.Models;
using Copmany.MVC.PL.Dto;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Copmany.MVC.PL.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public UserController(UserManager<AppUser> userManager ) 
        {
            _userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> Index(string? SearchInput)
        {
            IEnumerable<UserToReturnDto> users;
            if (string.IsNullOrEmpty(SearchInput))
            {
                users = _userManager.Users.Select(U => new UserToReturnDto()
                {
                    Id = U.Id,
                    UserName = U.UserName,
                    Email = U.Email,
                    FirstName = U.FirstName,
                    LastName = U.LastName,
                    Roles = _userManager.GetRolesAsync(U).Result
                });

            }
            else
            {
                users = _userManager.Users.Select(U => new UserToReturnDto()
                {
                    Id = U.Id,
                    UserName = U.UserName,
                    Email = U.Email,
                    FirstName = U.FirstName,
                    LastName = U.LastName,
                    Roles = _userManager.GetRolesAsync(U).Result
                }).Where(U => U.FirstName.ToLower().Contains(SearchInput.ToLower()));

           
            }
            return View(users);
        }


        [HttpGet]
        public async Task<IActionResult> Detelis(string? Id, string viewname = "Detelis")
        {
            if (Id is null) return BadRequest("Invaild");

            var user = await _userManager.FindByIdAsync(Id);
            if (user is null) return NotFound(new { StatusCode = 404, Message = $"user With Id is Not Found {Id} " });
            var dto = new UserToReturnDto()
            { 
                Id = user.Id,
                UserName = user.UserName,
                FirstName= user.FirstName,
                LastName= user.LastName,
                Email = user.Email,
                Roles = _userManager.GetRolesAsync(user).Result
            
            };
            return View(viewname, dto);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string? Id)
        {
            
            return await Detelis(Id,"Edit");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] string id, UserToReturnDto model)
        {
            if (ModelState.IsValid)
            {
                if (id != model.Id) return BadRequest();


                var user = await _userManager.FindByIdAsync(id);
                if (user  is null ) return BadRequest();
                
                user.UserName = model.UserName;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Email = model.Email;

                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string? Id)
        {
            return await Detelis(Id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] string id, UserToReturnDto model)
        {
            if (ModelState.IsValid)
            {
                if (id != model.Id) return BadRequest();


                var user = await _userManager.FindByIdAsync(id);
                if (user is null) return BadRequest();

                

                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);
        }
    }
}
