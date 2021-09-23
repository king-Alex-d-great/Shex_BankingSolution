using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineBanking.Domain.Entities;
using OnlineBanking.Domain.Interfaces.Repositories;
using OnlineBanking.Domain.UnitOfWork;
using WebUI.domain.Interfaces.Services;
using WebUI.domain.Model;

namespace WebUI.domain.Services
{
    public class AccountServices : IAccountService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAccountRepository _accountRepo;

        public AccountServices(IUnitOfWork unitOfWork, IAccountRepository accountRepo)
        {
            _unitOfWork = unitOfWork;
            _accountRepo = accountRepo;
        }

        public void checkBalance(User user)
        {
            throw new NotImplementedException();
        }       

        public Account Get(Guid Id)
        {
            return _accountRepo.Find(a => a.Id == Id).FirstOrDefault();
        }

        public Account Get(Customer customer)
        {
           return customer.Account;
        }
        public Account Get(string accountNumber)
        {
           return _accountRepo.Find(a => a.AccountNumber == accountNumber).FirstOrDefault();
        }
        

        public int Update(UpdateViewModel model)
        {
            throw new NotImplementedException();
        }
    }
}
