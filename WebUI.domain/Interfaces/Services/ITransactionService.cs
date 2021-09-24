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
        public (bool isAccountValid, bool isBalanceSufficient, bool isAccountActive, int affectedRows) Withdraw(WithdrawViewModel model);
        public (
            bool isSenderAccountValid,
            bool isReciepientAccountValid,
            bool isBalanceSufficient,
            bool isSenderAccountActive,
            bool isReciepientAccountActive,
            bool isReciepientAccountDifferent,
            bool isReciepientCustomerExistent,
            int affectedRows,
            bool willReduceBankMaintenanceFee
            ) Transfer(TransferViewModel model);
        public (bool isAccountValid, bool isAccountActive, int affectedRows) Deposit(DepositViewModel model);
        public IEnumerable<Transaction> GetAll();
    }
}
