using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using OnlineBanking.Domain.Entities;
using OnlineBanking.Domain.Interfaces.Services;
using OnlineBanking.Domain.Model;
using OnlineBanking.Domain.UnitOfWork;

namespace OnlineBanking.Domain.Services
{
    class UserService : IUserService
    {
        private readonly IUnitOfWork<User> _unitOfWork;

        public UserService(IUnitOfWork<User> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public User Delete(int id, out int affectedRow)
        {
            throw new NotImplementedException();
        }

        public void Get(int id)
        {
            throw new NotImplementedException();
        }

        public User Login(LoginViewModel model, out Account account)
        {
            throw new NotImplementedException();
        }

        public void Register(RegisterViewModel model)
        {
            throw new NotImplementedException();
        }

        public int Update(UpdateViewModel model, int Id)
        {
            throw new NotImplementedException();
        }
    }
}
