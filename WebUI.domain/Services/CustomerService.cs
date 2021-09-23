using System;
using System.Linq;
using System.Security.Cryptography;
using OnlineBanking.Domain.Entities;
using OnlineBanking.Domain.Enumerators;
using OnlineBanking.Domain.Interfaces.Repositories;
using OnlineBanking.Domain.UnitOfWork;
using WebUI.domain.Interfaces.Services;
using WebUI.domain.Model;
using OnlineBanking.Domain.Helpers.AccountGenerator;
using OnlineBanking.Domain.Helpers.AgeManager;

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
        public (bool isAgeMoreThanMinimumAge, bool isAgeLessThanMaximumAge, int AffectedRows) Add (EnrollCustomerViewModel enrollModel)
        {
            int AffectedRows = default;
            var isAgeValid = AgeValidator.isAgeValid(enrollModel.Birthday.Value);
            if (isAgeValid.isAgeMaximumAge == false || isAgeValid.isAgeMinimumAge == false) goto end;

            var customer = new Customer
            {
                UserId = enrollModel.ReadOnlyCustomerProps.UserId,
                Birthday = enrollModel.Birthday.Value,
                Gender = enrollModel.Gender,
                Account = new Account
                {
                    AccountType = enrollModel.AccountType,
                    AccountNumber = AccountNumberGenerator.GenerateAccountNumber(),
                    CreatedBy = enrollModel.ReadOnlyCustomerProps.CreatedBy,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    Balance = enrollModel.AccountType != AccountType.Savings ? 0 : 5000,
                    IsActive = true,
                },
            };
            _customerRepo.Add(customer);
            AffectedRows = _unitOfWork.Commit();
        end:
            return (isAgeValid.isAgeMinimumAge, isAgeValid.isAgeMaximumAge, AffectedRows);
        }
        public Customer GetCustomer(string userId)
        {
            return _customerRepo.Find(a => a.UserId == userId).FirstOrDefault();
        } 
        
        public Customer GetCustomer(Guid accountId)
        {
            return _customerRepo.Find(a => a.AccountId == accountId).FirstOrDefault();
        }

        public Customer GetCustomerWithAccount(string accountId)
        {
            return _customerRepo.Find(a => a.AccountId.ToString() == accountId).FirstOrDefault();
        }
    }
}
