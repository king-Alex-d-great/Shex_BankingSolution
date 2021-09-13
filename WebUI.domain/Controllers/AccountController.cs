using System;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineBanking.Domain.Entities;
using OnlineBanking.Domain.Interfaces.Services;
using OnlineBanking.Domain.Services;
using WebUI.domain.Model;

namespace WebUI.domain.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {

            _signInManager = signInManager;
            _userManager = userManager;
           
        }

        public UserManager<User> UserManger { get; }

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
                    Email = model.Email,
                    Password = model.Password,
                    UserName = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    IsActive = model.IsActive = true,                   
                    Customer = new Customer { UserName = model.Email },
                    Account = new Account
                    {
                        AccountNumber = (RandomNumberGenerator.GetInt32(1000, 9999) * RandomNumberGenerator.GetInt32(10000, 99999)).ToString(),
                        Balance = model.AccountType == OnlineBanking.Domain.Enumerators.AccountType.Savings ? 5000 : 0,
                        AccountType = model.AccountType,
                        CreatedAt= DateTime.Now,
                        IsActive = true,
                        UpdatedAt = DateTime.Now,  
                        CreatedBy= "king Alex",
                        UpdatedBy = "Shola nejo"
                    }

                };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
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

        public async Task<IActionResult> EnrollCustomer(EnrollCustomerViewModel model)
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

        }

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
