using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineBanking.Domain.Entities;
using OnlineBanking.Domain.Interfaces.Repositories;

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

        public async Task <IActionResult> Index()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return View(roles);
        }
        public async Task <IActionResult> AddRole(string RoleName)
        {
            if(RoleName != null)
            {
                await _roleManager.CreateAsync(new AppRole { Name = RoleName });
            }
            return RedirectToAction("Index");
        }
        public async Task <IActionResult> DeleteRole(string RoleName)
        {
                if (RoleName != null)
                {
                    await _roleManager.DeleteAsync(new AppRole { Name = RoleName });
                }
            return RedirectToAction("RemoveRole");
        }
        public async Task<IActionResult> UpdateRole(string RoleName)
        {
            if (RoleName != null)
            {
                await _roleManager.UpdateAsync(new AppRole { Name = RoleName });
            }
            return RedirectToAction("UpdateRole");
        }

    }
}
