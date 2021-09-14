using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineBanking.Domain.Entities;
using OnlineBanking.Domain.Enumerators;
using OnlineBanking.Domain.Interfaces.Repositories;
using OnlineBanking.Domain.Interfaces.Services;
using OnlineBanking.Domain.Services;
using OnlineBanking.Domain.UnitOfWork;
using WebUI.domain.Interfaces.Services;
using WebUI.domain.Model;
using OnlineBanking.Domain.Enumerators;

namespace WebUI.domain.Controllers
{
    public class AccountController : Controller
    {
        //should hold crud operation for user
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager, RoleManager<AppRole> roleManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _customerService = customerService;           
        }
        
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    FullName = $"{model.FirstName} {model.LastName}",
                    Email = model.Email,
                    UserName = model.Email,
                    CreatedBy= "Shola nejo",                  
                };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    await _userManager.AddToRoleAsync(user, Roles.Customer.ToString());

                    var roles = await _userManager.GetRolesAsync(user);
                    // var user = _userService.Get(model.Email);
                    return View("HomePage", (user, roles));                   
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View();
        }
        [HttpGet]
        public IActionResult LogIn()
        {
            return View();
        }

        public IActionResult EnrollCustomer()
        {
            return View(new EnrollCustomerViewModel());
        }

        public async Task<IActionResult> AddUser(AddUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var user = new User
            {
                FullName = $"{model.FirstName} {model.LastName}",
                Email = model.Email,
                UserName = model.Email
            };
            var result = await _userManager.CreateAsync(user, "Alex-1234");

            if (result.Succeeded)
            {
                return View("ViewAll");
            }
            ModelState.AddModelError(String.Empty, "Operation failed, try again!");
            return View();
        }
        public async Task<IActionResult> EnrollCustomer(EnrollCustomerViewModel model, User user)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }

            var customer = new Customer
            {
                UserId = user.Id,
                Birthday = model.Birthday,
                Gender = model.Gender,
                Account = new Account
                {
                    AccountType = model.AccountType,
                    UserId = user.Id,
                    CreatedAt = DateTime.Now,
                    CreatedBy = "Shola Nejo",
                    AccountNumber = RandomNumberGenerator.GetInt32(9998, 9999) * RandomNumberGenerator.GetInt32(99998, 99999),
                    UpdatedAt = DateTime.Now,
                }
            };
            var result = _customerService.Add(customer);
            if(result == null)
            {
                return View();
            }
            return View("ViewAll");
        }
       

        [HttpPost]
        public async Task<IActionResult> LogIn(LoginViewModel model)
        {

            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: model.RememberMe, false);
                if (result.Succeeded)
                {
                    var user = await _userManager.FindByNameAsync(model.Email);
                    var roles = await _userManager.GetRolesAsync(user);
                    await _userManager.GetUsersInRoleAsync("SuperAdmin");                    
                    return View("HomePage", (user, roles));
                }
                ModelState.AddModelError(String.Empty, "Invalid Login Attempt");
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("index", "Home");
        }

        public IActionResult HomePage((User, IList<string>) model)
        {
            return View(model);
        }
        public async Task<IActionResult> ViewAll()
        {
            var users = await _userManager.Users.ToListAsync();
            var viewAllViewModel = new List<DisplayAllViewModel>();
            foreach (User user in users)
            {
                var thisViewModel = new DisplayAllViewModel
                {
                    Email = user.Email,
                    UserId = user.Id,
                    FullName = user.FullName,
                    UserName = user.UserName,
                    Roles = await GetUserRoles(user)
                };
                viewAllViewModel.Add(thisViewModel);
            }
            return View(viewAllViewModel);
        }
        private async Task<List<string>> GetUserRoles(User user)
        {
            return new List<string>(await _userManager.GetRolesAsync(user));
        }

    }
}
