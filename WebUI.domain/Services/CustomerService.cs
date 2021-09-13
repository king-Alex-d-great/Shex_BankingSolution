﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineBanking.Domain.Entities;
using OnlineBanking.Domain.Interfaces.Repositories;
using OnlineBanking.Domain.UnitOfWork;
using WebUI.domain.Interfaces.Services;
using WebUI.domain.Models;

namespace WebUI.domain.Services
{
    public class CustomerService: ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICustomerRepository _customerRepo;

        public CustomerService(IUnitOfWork unitOfWork, ICustomerRepository customerRepo )
        {
            _unitOfWork = unitOfWork;
            _customerRepo = customerRepo;
        }

        public void Add(CustomerViewModel model)
        {
            /*var customer = new Customer
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Birthday = model.Birthday,
                Gender = model.Gender,
                AccountType = model.AccountType,

                Account = new Account()
            };

            _customerRepo.Add(customer);
            _unitOfWork.Commit();*/

        }
    }
}
