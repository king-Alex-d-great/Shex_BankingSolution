
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

        public (bool isAccountValid, bool isAccountActive, int affectedRows) Deposit(DepositViewModel model)
        {
            int affectedRows = 0;
            var account = _accountService.Get(model.customer.AccountId);
            var isAccountValid = account != null ? true : false;
            var isAccountActive = account?.IsActive != false ? true : false;

            if (isAccountValid == false || isAccountActive == false) goto end;

            account.Balance += model.Amount;
            var transaction = new Transaction { Amount = model.Amount, TimeStamp = System.DateTime.Now, TransactionMode = TransactionType.Credit, UserId = model.customer.UserId };
            _transactionRepo.Add(transaction);
            affectedRows = _unitOfWork.Commit();
        end:
            return (isAccountValid, isAccountActive, affectedRows);
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
            int affectedRows
            )  Transfer(TransferViewModel model)
        {
            //get two accounts
            var senderAccount = _accountService.Get(model.customer.AccountId);
            var reciepientAccount = _accountService.Get(model.ReciepientAccountNumber);
            var receipientCustomerAccount = _customerService.GetCustomerWithAccount(reciepientAccount?.Id.ToString()); 

            var affectedRows = 0;
            var isSenderAccountValid = senderAccount != null ? true : false;
            var isReciepientAccountValid = reciepientAccount != null ? true : false;
            var isReciepientCustomerExistent = receipientCustomerAccount != null ? true : false;
            var isBalanceSufficient = senderAccount?.Balance > model.Amount ? true : false;
            var isSenderAccountActive = senderAccount?.IsActive != false ? true : false;
            var isReciepientAccountActive = reciepientAccount?.IsActive == false ? false : true;
            var isReciepientAccountDifferent = reciepientAccount?.AccountNumber != senderAccount?.AccountNumber ? true : false;

            if (isSenderAccountValid == false ||
                isReciepientAccountValid == false ||
                isBalanceSufficient == false ||
                isSenderAccountActive == false ||
                isReciepientAccountActive == false ||
                isReciepientCustomerExistent == false ||
                isReciepientAccountDifferent == false) goto end;

            senderAccount.Balance -= model.Amount;
            reciepientAccount.Balance += model.Amount;
            var senderTransaction = new Transaction { Amount = model.Amount, TimeStamp = DateTime.Now, TransactionMode = TransactionType.Debit, UserId = model.customer.UserId };
            var reciepientTransaction = new Transaction { Amount = model.Amount, TimeStamp = DateTime.Now, TransactionMode = TransactionType.Credit, UserId = receipientCustomerAccount.UserId };
            _transactionRepo.Add(senderTransaction);
            _transactionRepo.Add(reciepientTransaction);
            affectedRows = _unitOfWork.Commit();
        end:
            return (isSenderAccountValid, isReciepientAccountValid, isBalanceSufficient, isSenderAccountActive, isReciepientAccountActive, isReciepientAccountDifferent,  isReciepientCustomerExistent,affectedRows);
 }

        public (bool isAccountValid, bool isBalanceSufficient, bool isAccountActive, int affectedRows,bool willReduceBankMaintenanceFee) Withdraw(WithdrawViewModel model)
        {
            int affectedRows = 0;
            var account = _accountService.Get(model.customer.AccountId);

            var isBalanceSufficient = account != null;
            var isAccountValid = account?.Balance > model?.Amount;
            var isAccountActive = account?.Balance > model?.Amount;
            bool willReduceBankMaintenanceFee = false;

            if (account.AccountType == AccountType.Savings && isBalanceSufficient)
            {
                willReduceBankMaintenanceFee = (account.Balance -= model.Amount) >= 5000;
            }

            if (isAccountValid == false || isBalanceSufficient == false || isAccountActive == false || willReduceBankMaintenanceFee == false) goto end;

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
