using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Whosales.Web.Models;
using Whosales.Web.Models.Users;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
using System.Linq;

namespace Whosales.Web.Controllers
{
    public class UsersController : Controller
    {
        UserManager<User> _userManager;
        RoleManager<IdentityRole> _roleManager;

        public UsersController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        [Route("Users")]
        public IActionResult Index() => View(_userManager.Users.ToList());
        [Route("Users/Create")]
        public IActionResult Create()
        {
            CreateUserViewModel model = new CreateUserViewModel
            {
                Roles = _roleManager.Roles.Select(x => new SelectListItem(x.Name, x.Name))
            };
            return View(model); 
        }

        [HttpPost]
        [Route("Users/Create")]
        public async Task<IActionResult> Create(CreateUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User { Email = model.Email, UserName = model.Email, Year = model.Year };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, model.SelectedRole);
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }
        [Route("Users/Edit")]
        public async Task<IActionResult> Edit(string id)
        {
            User user = await _userManager.FindByIdAsync(id);
            string userRole = (await _userManager.GetRolesAsync(user)).First();

            if (user == null)
            {
                return NotFound();
            }
            EditUserViewModel model = new EditUserViewModel
            {
                Id = user.Id,
                Email = user.Email,
                Year = user.Year,
                SelectedRole = userRole,
                Roles = _roleManager.Roles.Select(x => new SelectListItem(x.Name, x.Name))
            };
            return View(model);
        }

        [HttpPost]
        [Route("Users/Edit")]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _userManager.FindByIdAsync(model.Id);
                if (user != null)
                {
                    user.Email = model.Email;
                    user.UserName = model.Email;
                    user.Year = model.Year;

                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {

                        var userRoles = await _userManager.GetRolesAsync(user);
                        if (!userRoles.Contains(model.SelectedRole) || userRoles.Count > 1)
                        {
                            await _userManager.RemoveFromRolesAsync(user, userRoles);
                            await _userManager.AddToRoleAsync(user, model.SelectedRole);
                        }

                        return RedirectToAction("Index");

                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
            }
            return View(model);
        }

        [HttpPost]
        [Route("Users/Delete/{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            User user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await _userManager.DeleteAsync(user);
            }
            return RedirectToAction("Index");
        }
    }
}