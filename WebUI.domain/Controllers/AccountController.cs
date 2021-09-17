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
using SendGrid;
using SendGrid.Helpers.Mail;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using WebUI.domain.Middlewares;

/*using OnlineBanking.Domain.Enumerators;*/

namespace WebUI.domain.Controllers
{
    public class AccountController : Controller
    {
        //should hold crud operation for user
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
                    CreatedBy= "Shola nejo",                  
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
        /*public IActionResult LogIn()
        {
            return View();
        }
*/
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

            if (result.Item1.Succeeded) return RedirectToAction("ViewAll");
            
            ModelState.AddModelError(String.Empty, "Operation failed, try again!");
            return View();
        }

        [HttpGet]
        public IActionResult EnrollCustomer()
        {
            return View(new EnrollCustomerViewModel());
        }
        /*public IActionResult EnrollCustomer()
        {
            var model = (new EnrollCustomerViewModel { }, new AddUserViewModel { });
            return View(model);
        }*/


        /*public async Task<IActionResult> EnrollCustomer([FromBody](EnrollCustomerViewModel enrollModel,AddUserViewModel addModel) model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var result = await AddUser(model.addModel);

            if (!result.Item1.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "An Error Occurred, Customer not created!!");
                return View();
            }
             var AffectedRows =  _customerService.Add(model.enrollModel, result.Item2, new ClaimsViewModel { Username = User.Identity.Name, UserId = result.Item2.Id});

            if (AffectedRows < 1)
            {
                ModelState.AddModelError(string.Empty, "A Database error occured!!");
                return View();
            }
            return RedirectToAction("ViewAll");
        } */
        [HttpPost]
        public async Task<IActionResult> EnrollCustomer(EnrollCustomerViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var (result, user) = await AddUserAsync(new IdentityViewModel
                {Email = model.Email, FullName = $"{model.FirstName} {model.LastName}"});
            
            if (result.Succeeded)
            {
                model.ReadOnlyCustomerProps = new ReadOnlyCustomerProps
                {
                    UserId = user.Id,
                    CreatedBy = User.GetUserName(),
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now

                };

                _customerService.Add(model);

                TempData["EnrollSuccess"] = "Enrollment Was Successful!";

                //Send Mail To User With Credentials
                var apiKey = "SG.WA0Rvsa6RkCO_mRHtrkvHQ.ZGKJnm0lJIAQkf5dUbjcUdQLWCwZl - HxZFKUX2Da_8w";
                var client = new SendGridClient(apiKey);                
              var from = new EmailAddress("ogubuikealex@gmail.com", "SHeX");
                var subject = "Sending with SendGrid is Fun";
                var to = new EmailAddress("ogubuikealex@gmail.com", "SHeX");
                var plainTextContent = "and easy to do anywhere, even with C#";
                var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
                var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
                var response = await client.SendEmailAsync(msg);

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
        public  async Task<IActionResult> HomePage()
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

        [HttpPost]
        public async Task<IActionResult> DeleteUser(string userName)
        {
            if (userName != null)
            {
                await _userManager.DeleteAsync(new User { UserName = userName });
            }
            return RedirectToAction();

        }

        [HttpPost]
        public async Task<IActionResult> UpdateUser(User user)
        {
            if (user != null)
            {
                await _userManager.UpdateAsync(user);
            }
            return RedirectToAction();
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
                UserName = model.Email
            };
            var randPassword = "Alex-1234";

            var result = await _userManager.CreateAsync(user, randPassword);
            return (result, user);
        }

    }
}
