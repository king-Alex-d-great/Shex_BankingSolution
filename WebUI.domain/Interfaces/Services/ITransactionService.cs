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
        (bool isAccountValid, bool isBalanceSufficient, bool isAccountActive, int affectedRows, bool willReduceBankMaintenanceFee) Withdraw(WithdrawViewModel model);

        (
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
        (bool isAccountValid, bool isAccountActive, int affectedRows) Deposit(DepositViewModel model);

        (bool success, string message) DepositV2(DepositViewModel model);
        public IEnumerable<Transaction> GetAll();
    }
}
