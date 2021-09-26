using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineBanking.Domain.Entities;
using OnlineBanking.Domain.Model;
using WebUI.domain.Models.RoleControllerModels;

namespace WebUI.domain.Controllers
{
    [Authorize]
    public class RoleController : Controller
    {
        private readonly RoleManager<AppRole> _roleManager;
        private readonly UserManager<User> _userManager;
        //Hold Crud Operation For roles
        public RoleController(RoleManager<AppRole> roleManager, UserManager<User> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return View(roles);
        }

        [HttpPost]
        public async Task<IActionResult> AddRole(string RoleName)
        {
            if (ModelState.IsValid)
            {
                await _roleManager.CreateAsync(new AppRole { Name = RoleName });
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult DeleteRole(string Id)
        {
            return View("DeleteRole");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRole(EditRoleViewModel model)
        {
            try
            {
                var role = await _roleManager.FindByNameAsync(model.RoleName);
                if (role == null)
                {
                    ViewBag.ErrorMessage = $"Role with Name = {model.RoleName} cannot be found";
                }
                else
                {
                    await _roleManager.DeleteAsync(role);
                }
                return RedirectToAction("Index");
            }
            catch (System.Exception)
            {
                return RedirectToAction("DeleteRole");
            }

        }

        [HttpGet]
        public IActionResult UpdateRole(string Id)
        {
            return View("UpdateRole");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateRole(EditRoleViewModel model)
        {
            var role = await _roleManager.FindByIdAsync(model.Id);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {model.Id} cannot be found";
            }
            else
            {
                role.Name = model.RoleName;
                var result = await _roleManager.UpdateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> ManageUserRole(string userId)
        {
            ViewBag.userId = userId;
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {userId} cannot be found";
                return View("NotFound");
            }        
            ViewBag.UserName = user.UserName;
            var model = new List<ManageUserRolesViewModel>();
            foreach (var role in _roleManager.Roles)
            {
                var userRolesViewModel = new ManageUserRolesViewModel
                {
                    RoleId = role.Id,
                    RoleName = role.Name
                };
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    userRolesViewModel.Selected = true;
                }
                else
                {
                    userRolesViewModel.Selected = false;
                }
                model.Add(userRolesViewModel);
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> ManageUserRole(List<ManageUserRolesViewModel> model, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return View();
            }
            var roles = await _userManager.GetRolesAsync(user);
            var result = await _userManager.RemoveFromRolesAsync(user, roles);
            if (!result.Succeeded)
            {

                ModelState.AddModelError("", "Cannot remove user existing roles");
                return View(model);

            }
            result = await _userManager.AddToRolesAsync(user, model.Where(x => x.Selected).Select(y => y.RoleName));
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot add selected roles to user");
                return View(model);
            }
            return RedirectToAction("Index");
        }
    }
}

