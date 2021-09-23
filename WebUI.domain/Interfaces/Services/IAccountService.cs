

using System;
using OnlineBanking.Domain.Entities;
using WebUI.domain.Model;
using WebUI.domain.Models;

namespace WebUI.domain.Interfaces.Services
{
    public interface IAccountService
    {
        Account Get(string accountNumber);
        Account Get(Customer customer);
        public Account Get(Guid Id);
        void checkBalance(User user);
        int Update(UpdateViewModel model);        

        //(int affectedRow, Account account) Delete(int id);

    }
}