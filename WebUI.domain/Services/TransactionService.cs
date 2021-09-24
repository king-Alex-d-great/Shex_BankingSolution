
using System;
using System.Collections.Generic;
using OnlineBanking.Domain.Entities;
using OnlineBanking.Domain.Enumerators;
using OnlineBanking.Domain.Interfaces.Repositories;
using OnlineBanking.Domain.UnitOfWork;
using WebUI.domain.Interfaces.Services;
using WebUI.domain.Models.TransactionServiceModels;

namespace WebUI.domain.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITransactionRepository _transactionRepo;
        private readonly ICustomerService _customerService;
        private readonly IAccountService _accountService;

        public TransactionService(IUnitOfWork unitOfWork, ITransactionRepository transactionRepo, ICustomerService customerService, IAccountService accountService)
        {
            _unitOfWork = unitOfWork;
            _transactionRepo = transactionRepo;
            _accountService = accountService;
            _customerService = customerService;
        }


        public (bool success, string message) DepositV2(DepositViewModel model)
        {
           
            var account = _accountService.Get(model.customer.AccountId);
            var isAccountValid = account != null;
            var isAccountActive = account?.IsActive;


            if (!isAccountValid || !isAccountActive.Value)
            {
                var msg = !isAccountActive.Value ? "You cannot transfer" : "Your Account is Invalid";
                return (false, msg);
            }
            account.Balance += model.Amount;
            var transaction = new Transaction
            {

                Amount = model.Amount,
                TimeStamp = DateTime.Now,
                TransactionMode = TransactionType.Credit,
                UserId = model.customer.UserId
            };
            _transactionRepo.Add(transaction);

           var affectedRows = _unitOfWork.Commit();

           return affectedRows > 0 ? (true, "Deposit was Successful!") : (false, "An Error Occurred!!");
        }

        public (bool isAccountValid, bool isAccountActive, int affectedRows) Deposit(DepositViewModel model)
        {
            int affectedRows = 0;
            var account = _accountService.Get(model.customer.AccountId);
            var isAccountValid = account != null;
            var isAccountActive = account?.IsActive;

            if (!isAccountValid || !isAccountActive.Value) goto end;

            account.Balance += model.Amount;
            var transaction = new Transaction
            {
                
                Amount = model.Amount, 
                TimeStamp = System.DateTime.Now,
                TransactionMode = TransactionType.Credit, 
                UserId = model.customer.UserId
            };
            _transactionRepo.Add(transaction);
            affectedRows = _unitOfWork.Commit();

        end:
            return (isAccountValid, isAccountActive.Value, affectedRows);
        }

        //Transfer Method
        public (
            bool isSenderAccountValid,
            bool isReciepientAccountValid,
            bool isBalanceSufficient,
            bool isSenderAccountActive,
            bool isReciepientAccountActive,
            bool isReciepientAccountDifferent,
            bool isReciepientCustomerExistent,
            int affectedRows, bool willReduceBankMaintenanceFee
            ) 
            
            Transfer(TransferViewModel model)
        {
            //get two accounts
            var senderAccount = _accountService.Get(model.customer.AccountId);
            var reciepientAccount = _accountService.Get(model.ReciepientAccountNumber);
            var receipientCustomerAccount = _customerService.GetCustomerWithAccount(reciepientAccount?.Id.ToString()); 
            var affectedRows = 0;
            var isSenderAccountValid = senderAccount != null;
            var isReciepientAccountValid = reciepientAccount != null ;
            var isReciepientCustomerExistent = receipientCustomerAccount != null;
            var isBalanceSufficient = senderAccount?.Balance >= model.Amount;
            var isSenderAccountActive = senderAccount?.IsActive != false ;
            var isReciepientAccountActive = reciepientAccount?.IsActive;
            var isReciepientAccountDifferent = reciepientAccount?.AccountNumber != senderAccount?.AccountNumber ;
            bool willReduceBankMaintenanceFee = false;

            if (senderAccount.AccountType == AccountType.Savings && isBalanceSufficient)
            {
                willReduceBankMaintenanceFee = (senderAccount.Balance -= model.Amount) < 5000;
            }

            if (!isSenderAccountValid  ||
                !isReciepientAccountValid ||
                !isBalanceSufficient ||
                !isSenderAccountActive ||
                !isReciepientAccountActive.Value ||
                !isReciepientCustomerExistent||
                !isReciepientAccountDifferent) goto end;

            senderAccount.Balance -= model.Amount;
            reciepientAccount.Balance += model.Amount;
            var senderTransaction = new Transaction { Amount = model.Amount, TimeStamp = DateTime.Now, TransactionMode = TransactionType.Debit, UserId = model.customer.UserId };
            var reciepientTransaction = new Transaction { Amount = model.Amount, TimeStamp = DateTime.Now, TransactionMode = TransactionType.Credit, UserId = receipientCustomerAccount.UserId };
            _transactionRepo.Add(senderTransaction);
            _transactionRepo.Add(reciepientTransaction);
            affectedRows = _unitOfWork.Commit();
        end:
            return (isSenderAccountValid, isReciepientAccountValid, isBalanceSufficient, isSenderAccountActive, isReciepientAccountActive.Value, isReciepientAccountDifferent,  isReciepientCustomerExistent,affectedRows, willReduceBankMaintenanceFee);
 }

        public (bool isAccountValid, bool isBalanceSufficient, bool isAccountActive, int affectedRows,bool willReduceBankMaintenanceFee) Withdraw(WithdrawViewModel model)
        {
            int affectedRows = 0;
            var account = _accountService.Get(model.customer.AccountId);

            var isAccountValid= account != null;
            var isBalanceSufficient= account?.Balance >= model?.Amount;
            var isAccountActive = account.IsActive;
            bool willReduceBankMaintenanceFee = false;

            if (account.AccountType == AccountType.Savings && isBalanceSufficient)
            {
                willReduceBankMaintenanceFee = (account.Balance -= model.Amount) < 5000 ? true : false;
            }

            if (isAccountValid == false || isBalanceSufficient == false || isAccountActive == false || willReduceBankMaintenanceFee == true) goto end;

            account.Balance -= model.Amount;
            var transaction = new Transaction { Amount = model.Amount, TimeStamp = System.DateTime.Now, TransactionMode = TransactionType.Debit, UserId = model?.customer?.UserId };
            _transactionRepo.Add(transaction);
            affectedRows = _unitOfWork.Commit();
            end:
            return (isAccountValid, isBalanceSufficient, isAccountActive, affectedRows, willReduceBankMaintenanceFee);
        }

        public IEnumerable<Transaction> GetAll()
        {
           return _transactionRepo.GetAll();           
        }
    }
}
