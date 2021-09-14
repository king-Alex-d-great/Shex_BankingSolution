﻿

using OnlineBanking.Domain.Entities;
using OnlineBanking.Domain.Model;

namespace OnlineBanking.Domain.Interfaces.Services
{
    public interface IAccountService
    {
        Account Get(int userId);
        public Account Get(string Id);
        void checkBalance(User user);
        Account Register(RegisterViewModel model);
        int Update(UpdateViewModel model, int Id);
        Account Login(LoginViewModel model);
        (int affectedRow, Account account) Delete(int id);
       
    }
}