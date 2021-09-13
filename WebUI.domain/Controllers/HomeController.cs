using Microsoft.AspNetCore.Mvc;
using OnlineBanking.Domain.Interfaces.Services;
using OnlineBanking.Domain.Model;

namespace WebUI.domain.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAccountService _accountService;

        public HomeController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SignUp(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View();
            var account = _accountService.Register(model);
            return View("HomePage", account);
        }
        public IActionResult LogOut()
        {
            return View("Index");
        }
        [HttpGet]
        public IActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        public IActionResult LogIn(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                System.Console.WriteLine("grrr");
                return View();
            }

            System.Console.WriteLine("brrr");
            /* //access db and use login details to get account and pass that into view*/
            var result = _accountService.Login(model);
            return View("HomePage", result);
        }
        private IActionResult HomePage()
        {
            return View();
        }
    }
}