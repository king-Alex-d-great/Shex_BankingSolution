using System.Linq;
using Microsoft.AspNetCore.Mvc;
using OnlineBanking.Domain.Entities;

namespace WebUI.domain.Controllers
{
    public class HomeController : Controller
    {
        AppDbContext dbContext = new AppDbContext();
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
        public IActionResult SignUp(Account account)
        {
            if (ModelState.IsValid)
            {
               
               return View("HomePage", account);
            }
            else
            {
               return View();
            }
        }
        [HttpGet]
        public IActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        public IActionResult LogIn(LoginDetail loginDetail)
        {
            if (!ModelState.IsValid)
                return View();
            //access db and use login details to get acoount and pass that into view
            var account = dbContext.Accounts.Where(a => a.Email == loginDetail.Email && a.ConfirmPassword == loginDetail.Password);
            return View("HomePage", account);           
        }
        public IActionResult HomePage()
        {
            return View();
        }
    }
}