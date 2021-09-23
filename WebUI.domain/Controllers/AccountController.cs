using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineBanking.Domain.Entities;
using OnlineBanking.Domain.Enumerators;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebUI.domain.Interfaces.Services;
using WebUI.domain.Middlewares;
using WebUI.domain.Model;
using WebUI.domain.Models;

/*using OnlineBanking.Domain.Enumerators;*/

namespace WebUI.domain.Controllers
{
    public class AccountController : Controller
    {

        private readonly UserManager<User> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ICustomerService _customerService;

        public AccountController(UserManager<User> userManager, RoleManager<AppRole> roleManager, SignInManager<User> signInManager, ICustomerService customerService)
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
                    CreatedBy = "Shola nejo",
                };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    await _userManager.AddToRoleAsync(user, Roles.Customer.ToString());

                    // var roles = await _userManager.GetRolesAsync(user);
                    // var user = _userService.Get(model.Email);
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
        public async Task<IActionResult> LogIn(string returnUrl = null)
        {

            returnUrl ??= Url.Content("~/");
            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            ViewBag.ReturnUrl = returnUrl;
            return View();
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

                    return RedirectToAction("HomePage");
                }

            }
            ModelState.AddModelError(String.Empty, "Invalid Login Attempt");
            return View(model);
        }

        [HttpGet]
        public IActionResult EnrollUser()
        {
            return View(new AddUserViewModel());
        }


        [HttpPost]
        public async Task<IActionResult> EnrollUser(AddUserViewModel model)
        {
            if (!ModelState.IsValid) return View();
            var result = await AddUser(model);

            if (result.Item1.Succeeded)
                return RedirectToAction("ViewAll");

            ModelState.AddModelError(String.Empty, "Operation failed, try again!");
            return View();
        }

        [HttpGet]
        public IActionResult EnrollCustomer()
        {
            return View(new EnrollCustomerViewModel());
        }


        [HttpPost]
        public async Task<IActionResult> EnrollCustomer(EnrollCustomerViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var (result, user) = await AddUserAsync(new IdentityViewModel
            { Email = model.Email, FullName = $"{model.FirstName} {model.LastName}" });

            if (result.Succeeded)
            {
                model.ReadOnlyCustomerProps = new ReadOnlyCustomerProps
                {
                    UserId = user.Id,
                    CreatedBy = User.GetUserName(),
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now

                };
                await _userManager.AddToRoleAsync(user, Roles.Customer.ToString());
                _customerService.Add(model);

                TempData["EnrollSuccess"] = "Enrollment Was Successful!";

                //Send Mail To User With Credentials
                /*var apiKey = "SG.WA0Rvsa6RkCO_mRHtrkvHQ.ZGKJnm0lJIAQkf5dUbjcUdQLWCwZl - HxZFKUX2Da_8w";
                var client = new SendGridClient(apiKey);                
              var from = new EmailAddress("ogubuikealex@gmail.com", "SHeX");
                var subject = "Sending with SendGrid is Fun";
                var to = new EmailAddress("ogubuikealex@gmail.com", "SHeX");
                var plainTextContent = "and easy to do anywhere, even with C#";
                var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
                var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
                var response = await client.SendEmailAsync(msg);*/

                return RedirectToAction("ViewAll");

            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("index", "Home");
        }


        [Authorize]
        public async Task<IActionResult> HomePage()
        {
            var model = await _userManager.FindByIdAsync(User.GetUserId());
            return View(model);
        }
        public async Task<IActionResult> ViewAll()
        {

            return View(await _userManager.Users.ToListAsync());
        }
        private async Task<List<string>> GetUserRoles(User user)
        {
            return new List<string>(await _userManager.GetRolesAsync(user));
        }

        [HttpGet]
        public IActionResult DeleteUser()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> DeleteUser(string Id)
        {
            try
            {
                if (Id == null)
                {
                    return RedirectToAction("DeleteUser");
                }
                if (Id != null)
                {
                    var user = await _userManager.FindByIdAsync(Id);
                    await _userManager.DeleteAsync(user);
                }
                return RedirectToAction("ViewAll");
            }
            catch (System.Exception)
            {
                return RedirectToAction("DeleteRole");
            }
        }

        [HttpGet]
        public IActionResult UpdateUser(string Id)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUser(UpdateViewModel model)
        {

            var currentUserId = await _userManager.FindByIdAsync(model.Id);
            
            currentUserId.Email = model.Email;
            currentUserId.FullName = $"{model.FirstName} {model.LastName}";

            await _userManager.UpdateAsync(currentUserId);
            return RedirectToAction("ViewAll");
        }



        public async Task<(IdentityResult, User)> AddUser(AddUserViewModel model)
        {
            var user = new User
            {
                FullName = $"{model.FirstName} {model.LastName}",
                Email = model.Email,
                UserName = model.Email
            };
            // var defaultPassword = new Guid().ToString("N").Substring(0, 8);
            var result = await _userManager.CreateAsync(user, "Alex-1234");
            return (result, user);
        }


        private async Task<(IdentityResult result, User user)> AddUserAsync(IdentityViewModel model)
        {

            var user = new User
            {
                FullName = model.FullName,
                Email = model.Email,
                UserName = model.Email,
                StillHasDefaultPassword = true,

            };
            var randPassword = "Alex-1234";

            var result = await _userManager.CreateAsync(user, randPassword);
            return (result, user);
        }

    }
}
