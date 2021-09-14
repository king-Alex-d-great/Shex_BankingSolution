using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineBanking.Domain.Entities;

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
            return View("Index", roles);
        }
        [HttpPost]
        public async Task<IActionResult> AddRole(string RoleName)
        {
            if (!string.IsNullOrEmpty(RoleName))
            {
                await _roleManager.CreateAsync(new AppRole { Name = RoleName.Trim(), CreatedAt= DateTime.Now, CreatedBy="Shola Nejo" });
            }
            return RedirectToAction("Index", "Role");
        }
    }
}
