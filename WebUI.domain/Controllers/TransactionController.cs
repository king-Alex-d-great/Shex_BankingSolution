using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineBanking.Domain.Entities;
using WebUI.domain.Interfaces.Services;
using WebUI.domain.Middlewares;
using WebUI.domain.Models.TransactionServiceModels;

namespace WebUI.domain.Controllers
{
    [Authorize]
    public class TransactionController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ICustomerService _customerService;
        private readonly ITransactionService _transactionService;

        public TransactionController(
            UserManager<User> userManager, 
            RoleManager<AppRole> roleManager,
            SignInManager<User> signInManager, 
            ICustomerService customerService, 
            ITransactionService transactionService
            )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _customerService = customerService;
            _transactionService = transactionService;
        }
        public IActionResult Withdraw()
        {
            return View(new WithdrawViewModel());
        }
        [HttpPost]
        [Authorize]
        public IActionResult Withdraw(WithdrawViewModel model)
        {
            try
            {
                if (!ModelState.IsValid) return View();

                model.customer = _customerService.GetCustomer(User.GetUserId());
                var (success, message) = _transactionService.Withdraw(model);
                if (!success)
                {
                    ModelState.AddModelError(string.Empty, message);
                    return View();
                }
                
                TempData["WithdrawSuccess"] = message;
                return RedirectToAction("HomePage", "Account");
            }
            catch (Exception error)
            {
                ModelState.AddModelError(String.Empty, error.Message);
                return View();
            }
        }
        public IActionResult Deposit()
        {
            return View(new DepositViewModel());
        }
        [HttpPost]        
        public IActionResult Deposit(DepositViewModel model)
        {
            try
            {
                if (!ModelState.IsValid) return View();

                model.customer = _customerService.GetCustomer(User.GetUserId());
                var (success, message) = _transactionService.Deposit(model);
                if (!success) { ModelState.AddModelError(String.Empty, message); return View(); }

                TempData["DepositSuccess"] = message;
                return RedirectToAction("HomePage", "Account");
            } catch (Exception error)
            {
                ModelState.AddModelError(string.Empty, error.Message);
                return View();
            }
        }
        [HttpGet]
        public IActionResult Transfer()
        {            
            return View(new TransferViewModel());
        }
        [HttpPost]
        public IActionResult Transfer(TransferViewModel model)
        {
            try
            {
                if (!ModelState.IsValid) return View();
                var userId = User.GetUserId();
                model.customer = _customerService.GetCustomer(userId);
                
                var (success, message) = _transactionService.Transfer(model);
                if (!success) { ModelState.AddModelError(String.Empty, message); return View(); }                

                TempData["TransferSuccess"] = message;
                return RedirectToAction("HomePage", "Account");
            }
            catch (Exception error)
            {
                ModelState.AddModelError(String.Empty, error.Message);
                return View();
            };
        }

        public IActionResult TransactionHistory()
        {
           var transactions = _transactionService.GetAll();
            return View();
        }
        [Authorize]
        public IActionResult SuccessPage()
        {          
            return View();
        }
    }
}
