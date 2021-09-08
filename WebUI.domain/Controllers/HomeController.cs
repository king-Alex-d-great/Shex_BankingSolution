using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineBanking.Domain.Entities;
using OnlineBanking.Domain.Repositories;
using OnlineBanking.Domain.UnitOfWork;

namespace WebUI.domain.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext unitOfWork;

        public HomeController(AppDbContext dbContext)
        {
            unitOfWork = dbContext;
        }

        //private readonly IUnitOfWork unitOfWork;

        //public HomeController(IUnitOfWork _unitOfWork)
        //{
        //    unitOfWork = _unitOfWork;
        //}

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [Route("Register")]
        public IActionResult Register(Customer customerModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }
                else
                {
                    unitOfWork.Customers.Add(customerModel);
                    unitOfWork.SaveChanges();
                    return RedirectToAction("Index");
                }                
            }
            catch (Exception)
            {
                return View();
            }
        }

    }
}
