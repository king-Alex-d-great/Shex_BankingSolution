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
            if (RoleName != null)
            {
                var role = await _roleManager.FindByNameAsync(RoleName);
                await _roleManager.DeleteAsync(role);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult UpdateRole()
        {
            return View("UpdateRole");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateRole(EditRoleViewModel model)
        {

            if(model.RoleName != null)
            {
                var role = await _roleManager.FindByNameAsync(model.RoleName);
                model.RoleName = model.NewRoleName;
                /*var newName = new AppRole
                {
                    Name = role.Name
                };*/
                await _roleManager.UpdateAsync(role);
            }
            return RedirectToAction("Index");
        }
    }
}
