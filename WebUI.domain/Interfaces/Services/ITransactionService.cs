using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineBanking.Domain.Entities;
using WebUI.domain.Models.TransactionServiceModels;

namespace WebUI.domain.Interfaces.Services
{
    public interface ITransactionService
    {   
        public (bool success, string message) Withdraw(WithdrawViewModel model);
        (bool success, string message) Deposit (DepositViewModel model);
        (bool success, string message) Transfer (TransferViewModel model);
        public IEnumerable<Transaction> GetAll();
        public IEnumerable<Transaction> GetForSpecificUser(string UserId);
    }
}
