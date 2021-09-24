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
                //this page if authorized by roles, if user is not a customer cannot withdraw!
                //redirect pr
                var validation = _transactionService.Withdraw(model);

                if (validation.isAccountValid == false) { ModelState.AddModelError(String.Empty, "Your Account is Invalid,Please visit the branch you opened your account for clarification "); return View(); }
                if (validation.isAccountActive == false) { ModelState.AddModelError(String.Empty, "This Account is inactive, Please visit the branch you opened your account for clarification"); return View(); }
                if (validation.isBalanceSufficient == false) { ModelState.AddModelError(String.Empty, "Insufficient funds"); return View(); }
                if (validation.willReduceBankMaintenanceFee == true) { ModelState.AddModelError(String.Empty, "Insufficient funds!A maintenance fee of $5000 is required for a savings account "); return View(); }
                
                if (validation.affectedRows < 1) { ModelState.AddModelError(String.Empty, "An error ocurred\ntransaction unsuccessful, Please try again"); return View(); }
                
                TempData["WithdrawSuccess"] = "Withdrawal Successful!";
                return RedirectToAction("SuccessPage", "Transaction");
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
                //this page if authorized by roles, if user is not a customer cannot withdraw!
                //redirect pr
                var (success, msg) = _transactionService.DepositV2(model);
                // var validation = _transactionService.Deposit(model);
                //
                // if (validation.isAccountValid == false) { ModelState.AddModelError(String.Empty, "Your Account is Invalid,Please visit the branch you opened your account for clarification "); return RedirectToAction("HomePage", "Account"); }
                // if (validation.isAccountActive == false) { ModelState.AddModelError(String.Empty, "This Account is inactive, Please visit the branch you opened your account for clarification"); return RedirectToAction("HomePage", "Account"); }
                // if (validation.affectedRows < 1) { ModelState.AddModelError(String.Empty, "An error ocurred\ntransaction unsuccessful, Please try again"); return View(); }

                if (!success)
                {
                    ModelState.AddModelError(string.Empty, msg);
                    return View();
                }
                TempData["WithdrawSuccess"] = msg;
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
                //this page if authorized by roles, if user is not a customer cannot withdraw!
                //redirect pr
                var validation = _transactionService.Transfer(model);

                if (validation.isReciepientAccountValid == false) { ModelState.AddModelError(String.Empty, "Invalid Reciepient!"); return View(); }
                if (validation.isSenderAccountValid == false) { ModelState.AddModelError(String.Empty, "Your Account is Invalid,Please visit the branch you opened your account for clarification "); return View(); }
                if (validation.isSenderAccountActive == false) { ModelState.AddModelError(String.Empty, "Your Account has been deactivated, Please visit any of our branches for clarification"); return View(); }
                if (validation.isReciepientAccountActive == false) { ModelState.AddModelError(String.Empty, "You cannot transfer to this Account, because it is either invalid or has been deactivated!"); return View(); ; }
                if (validation.isBalanceSufficient == false) { ModelState.AddModelError(String.Empty, "Insuffiecient funds"); return View(); }
                if (validation.willReduceBankMaintenanceFee == true) { ModelState.AddModelError(String.Empty, "Insufficient funds!A maintenance fee of $5000 is required for a savings account "); return View(); }

                if (validation.isReciepientAccountDifferent == false) { ModelState.AddModelError(String.Empty, "You cannot transfer to yourself!"); return View(); }
                if (validation.isReciepientCustomerExistent == false) { ModelState.AddModelError(String.Empty, "Recipient customer not found!"); return View(); }
                if (validation.affectedRows < 1) { ModelState.AddModelError(String.Empty, "An error ocurred\ntransaction unsuccessful, Please try again"); return View(); }


                TempData["TransferSuccess"] = "Transfer Successful!";
                return RedirectToAction("SuccessPage", "Transaction");
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
