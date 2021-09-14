﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using OnlineBanking.Domain.Entities;
using OnlineBanking.Domain.Interfaces.Services;
using OnlineBanking.Domain.Services;
using WebUI.domain.Model;
using OnlineBanking.Domain.Enumerators;
using Microsoft.EntityFrameworkCore;

namespace WebUI.domain.Controllers
{
    public class AccountController : Controller
    {
        //should hold crud operation for user
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly SignInManager<User> _signInManager;

        [HttpPost]
        public async Task<IActionResult> CreateUser(User user)
        {
            if(user != null)
            {
                await _userManager.CreateAsync(user);
            }
            return RedirectToAction();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(string userName)
        {
            if(userName != null)
            {
                await _userManager.DeleteAsync(new User { UserName = userName });
            }
            return RedirectToAction();
            
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUser(User user)
        {
            if(user != null)
            {
                await _userManager.UpdateAsync(user);
            }
            return RedirectToAction();
        }

        public async Task <IActionResult> GetAllUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            return View(users);
        }


        public AccountController(UserManager<User> userManager, RoleManager<AppRole> roleManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
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
                    UserName = model.Email
                };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    await _userManager.AddToRoleAsync(user, Roles.Customer.ToString());
                    return View("HomePage", user);
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

        /*public async Task<IActionResult> EnrollCustomer(EnrollCustomerViewModel model)
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
            var result = await _userManager.CreateAsync(user, new Guid().ToString("N").Substring(0,8));
            if (result.Succeeded)
            {
                new Customer
                {
                    UserId = user.Id,
                    Birthday = model.Birthday,
                    Gender = model.Gender,
                    Account = new Account
                    {
                        AccountType = model.AccountType,

                        
                    }
                }
            }

        }*/

        [HttpPost]
        public async Task<IActionResult> LogIn(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: model.RememberMe, false);
                if (result.Succeeded)
                {
                   // var user = _userService.Get(model.Email);
                    return RedirectToAction("Index", "Home");
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

    }
}
