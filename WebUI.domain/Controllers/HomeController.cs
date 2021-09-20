using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineBanking.Domain.Entities;
using OnlineBanking.Domain.Repositories;
using OnlineBanking.Domain.UnitOfWork;
using WebUI.domain.Interfaces.Services;


namespace WebUI.domain.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICustomerService _customerService;

        public HomeController(ICustomerService customerService)
        {
            _customerService = customerService;
        }


        public IActionResult Index()
        {
            return View();
        }   

    }
}