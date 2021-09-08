using System.Linq;
using Microsoft.AspNetCore.Mvc;
using OnlineBanking.Domain.Entities;
using OnlineBanking.Domain.Interfaces.Services;
using OnlineBanking.Domain.Model;
using OnlineBanking.Domain.Services;
using OnlineBanking.Domain.UnitOfWork;

namespace WebUI.domain.Controllers
{
    public class HomeController : Controller
    {
        static IAccountService AccountService = new AccountService(new UnitOfWork<Account>(new AppDbContext()));

        private IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        private IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        private IActionResult SignUp(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
             return View();
            var account = AccountService.Register(model); 
            return View("HomePage", account);
        }
        private IActionResult LogOut()
        {
            return View("Index");
        }
        [HttpGet]
        private IActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        private IActionResult LogIn(LoginViewModel model)
        {            
            if (!ModelState.IsValid)
                return View();
            //access db and use login details to get acoount and pass that into view
            var account = AccountService.Login(model, out Account LoginAccount);
            return View("HomePage", LoginAccount);           
        }
        private IActionResult HomePage()
        {
            return View();
        }
    }
}