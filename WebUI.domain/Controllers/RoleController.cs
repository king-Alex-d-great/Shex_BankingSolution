using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineBanking.Domain.Entities;
using OnlineBanking.Domain.Model;
using System.Threading.Tasks;

namespace WebUI.domain.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<AppRole> _roleManager;

        //Hold Crud Operation For roles
        public RoleController(RoleManager<AppRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return View(roles);
        }
        public async Task<IActionResult> AddRole(string RoleName)
        {
            if (ModelState.IsValid)
            {
                await _roleManager.CreateAsync(new AppRole { Name = RoleName });
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRole(string RoleName)
        {
            try
            {
                if (RoleName != null)
                {
                    var role = await _roleManager.FindByNameAsync(RoleName);
                    await _roleManager.DeleteAsync(role);
                }
                return RedirectToAction("Index");
            }
            catch (System.Exception)
            {
                return RedirectToAction("Index");
            }
           
        }

        [HttpGet]
        public async Task<IActionResult> UpdateRole(string Id)
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
    }
}

