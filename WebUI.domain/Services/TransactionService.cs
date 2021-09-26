
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
        public (bool success, string message) Deposit(DepositViewModel model)
        {

            var account = _accountService.Get(model.customer.AccountId);
            var isAccountValid = account != null;
            var isAccountActive = account?.IsActive;

            if (!isAccountValid || !isAccountActive.Value)
            {
                var msg = !isAccountActive.Value ? "This Account is inactive, Please visit the branch you opened your account for clarification" : "Your Account is Invalid,Please visit the branch you opened your account for clarification ";
                return (false, msg);
            }

            account.Balance += model.Amount;
            var transaction = new Transaction
            {
                Amount = model.Amount,
                TimeStamp = DateTime.Now,
                TransactionMode = TransactionMode.Credit,
                TransactionType = TransactionType.Deposit,
                UserId = model.customer.UserId,
                ReceiverAccount = account.AccountNumber,
                SenderAccount = account.AccountNumber
            };
            _transactionRepo.Add(transaction);

            var affectedRows = _unitOfWork.Commit();

            return affectedRows > 0 ? (true, "Your Deposit was Successful!") : (false, "An error ocurred\nDeposit unsuccessful, Please try again");
        }

        public (bool success, string message) Transfer(TransferViewModel model)
        {

            var senderAccount = _accountService.Get(model.customer.AccountId);
            var reciepientAccount = _accountService.Get(model.ReciepientAccountNumber);
            var receipientCustomerAccount = _customerService.GetCustomerWithAccount(reciepientAccount?.Id.ToString());
            var isSenderAccountValid = senderAccount != null;
            var isReciepientAccountValid = reciepientAccount != null;
            var isBalanceSufficient = senderAccount?.Balance >= model.Amount;
            var isSenderAccountActive = senderAccount?.IsActive;
            var isReciepientAccountActive = reciepientAccount?.IsActive;
            var isReciepientAccountDifferent = reciepientAccount?.AccountNumber != senderAccount?.AccountNumber;

            var message = !isSenderAccountValid ? "Your account is Invalid,Please visit the branch you opened your account for clarification" :
                       !isSenderAccountActive.Value ? "This account is inactive, Please visit the branch you opened your account for clarification" :
                       !isReciepientAccountValid ? "Reciepient account is Invalid" :
                       !isReciepientAccountActive.Value ? "Reciepient account is inactive" :
                       !isReciepientAccountDifferent ? "You cannot transfer to yourself" :
                       !isBalanceSufficient ? "Insufficient funds" :
                       (senderAccount.Balance -= model.Amount) < 5000 ? "Insufficient funds!A maintenance fee of $5000 is required for a savings account" :
                       string.Empty;

            if (!string.IsNullOrEmpty(message)) return (false, message);

            senderAccount.Balance -= model.Amount;
            reciepientAccount.Balance += model.Amount;
               
            var senderTransaction = new Transaction { 
                Amount = model.Amount, 
                TimeStamp = DateTime.Now,
                TransactionMode = TransactionMode.Debit,
                UserId = model.customer.UserId, 
                TransactionType = TransactionType.Transfer,
                ReceiverAccount = reciepientAccount.AccountNumber,
                SenderAccount = senderAccount.AccountNumber 
            };
            var reciepientTransaction = new Transaction { 
                Amount = model.Amount, 
                TimeStamp = DateTime.Now,
                TransactionMode = TransactionMode.Credit,
                UserId = receipientCustomerAccount.UserId,
                TransactionType = TransactionType.Transfer,
                ReceiverAccount = reciepientAccount.AccountNumber,
                SenderAccount = senderAccount.AccountNumber
            };
            _transactionRepo.Add(senderTransaction);
            _transactionRepo.Add(reciepientTransaction);
            var affectedRows = _unitOfWork.Commit();
            return affectedRows > 0 ? (true, "Your Transfer was Successful") : (false, "An error ocurred\nDeposit unsuccessful, Please try again");
        }

        public (bool success, string message) Withdraw(WithdrawViewModel model)
        {

            var account = _accountService.Get(model.customer.AccountId);
            var isAccountValid = account != null;
            var isBalanceSufficient = account?.Balance >= model?.Amount;
            var isAccountActive = account.IsActive;

            var msg = !isAccountValid ? "Your Account is Invalid,Please visit the branch you opened your account for clarification" :
                        !isAccountActive ? "This Account is inactive, Please visit the branch you opened your account for clarification" :
                        !isBalanceSufficient ? "Insufficient funds" :
                        (account.Balance -= model.Amount) < 5000 ? "Insufficient funds!A maintenance fee of $5000 is required for a savings account" :
                        string.Empty;

            if (!string.IsNullOrEmpty(msg)) return (false, msg);
            account.Balance -= model.Amount;
            var transaction = new Transaction { Amount = model.Amount, TimeStamp = System.DateTime.Now, TransactionMode = TransactionMode.Debit, UserId = model?.customer?.UserId , TransactionType = TransactionType.Withdrawal , ReceiverAccount = account.AccountNumber, SenderAccount = account.AccountNumber};
            _transactionRepo.Add(transaction);
            var affectedRows = _unitOfWork.Commit();
            return affectedRows > 0 ? (true, "Your Withdrawal was successful") : (false, "An error ocurred\nWithdrawal unsuccessful, Please try again");
        }

        public IEnumerable<Transaction> GetAll()
        {
            return _transactionRepo.GetAll();
        }

        public IEnumerable<Transaction> GetForSpecificUser(string UserId)
        {
            return _transactionRepo.Find(a => a.UserId == UserId);
        }
    }
}
