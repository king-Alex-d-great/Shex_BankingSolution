using System;
using System.Security.Cryptography;
using OnlineBanking.Domain.Entities;
using OnlineBanking.Domain.Enumerators;
using OnlineBanking.Domain.Interfaces.Repositories;
using OnlineBanking.Domain.UnitOfWork;
using WebUI.domain.Interfaces.Services;
using WebUI.domain.Model;
using WebUI.domain.Models;

namespace WebUI.domain.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICustomerRepository _customerRepo;

        public CustomerService(IUnitOfWork unitOfWork, ICustomerRepository customerRepo)
        {
            _unitOfWork = unitOfWork;
            _customerRepo = customerRepo;
        }

        public int? Add(Customer customer)
        {
            _customerRepo.Add(customer);
            int? result = _unitOfWork.Commit();
            return result;
        }
        public int Add(EnrollCustomerViewModel enrollModel, User user, ClaimsViewModel claimsModel)
        {
            var customer = new Customer
            {
                UserId = user.Id,
                Birthday = enrollModel.Birthday,
                Gender = enrollModel.Gender,
                Account = new Account
                {

                    AccountType = enrollModel.AccountType,
                    AccountNumber = RandomNumberGenerator.GetInt32(9998, 9999) * RandomNumberGenerator.GetInt32(99998, 99999),
                    CreatedBy = claimsModel.Username,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    Balance = enrollModel.AccountType != AccountType.Savings ? 0 : 5000
                },

            };

            _customerRepo.Add(customer);
            return _unitOfWork.Commit();

        }

       

        public void Add(CustomerViewModel model)
        {
            throw new NotImplementedException();
        }
    }
}
