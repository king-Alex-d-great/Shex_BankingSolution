using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineBanking.Domain.Entities;
using OnlineBanking.Domain.Interfaces.Repositories;
using OnlineBanking.Domain.UnitOfWork;
using WebUI.domain.Interfaces.Services;
using WebUI.domain.Model;
using WebUI.domain.Models;

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

        /*public void Withdraw(WithdrawalViewModel model)
        {
            try
            {
                var account = _accountRepo.Find(a => a.AccountNumber == model.AccountNumber).FirstOrDefault();
                if (account == null)
                {
                    Console.WriteLine("Invalid account number"); return;
                }

                if (Math.Sign(model.Amount) != 1) { Console.WriteLine(model.Amount); Console.WriteLine(" invalid amount"); return; }
                if (account.Balance < model.Amount)
                {
                    Console.WriteLine("Insufficient balance"); return;
                }
                account.Balance -= model.Amount;
                Console.WriteLine("\nIn what denominations will you like your cash to be \n1. 1000 Naira\n2. 500 Naira");
                int UserReply = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Collect your cash\n");// CurrrencyNoteCount //ADD CoLOR

                if (UserReply == 1)
                {
                    Console.WriteLine($"There are {model.Amount % 1000} notes of 1000 Naira , amounting to {model.Amount } \n");
                }
                if (UserReply == 2)
                {
                    Console.WriteLine($"There are {model.Amount % 500} notes of 500 Naira , amounting to {model.Amount} \n");
                }
               // _unitOfWork.Transactions.Add(new Transaction { Amount = model.Amount, TimeStamp = DateTime.Now, TransactionMode = TransactionMode.Debit, UserId = account.Id });
                _unitOfWork.Commit();
                Console.WriteLine($"Debit Alert");
                Console.WriteLine($"withdrew {model.Amount} from kings ATM");
                Console.WriteLine($"New Account Balance : {account.Balance}");
                return;
            }

            catch (Exception error)
            {
                Console.WriteLine(error.Message);
            }
        }*/
    }
}
