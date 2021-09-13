using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using OnlineBanking.Domain.Entities;
using OnlineBanking.Domain.Interfaces.Repositories;
using OnlineBanking.Domain.Interfaces.Services;
using OnlineBanking.Domain.Model;
using OnlineBanking.Domain.UnitOfWork;

namespace OnlineBanking.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepo;
       
        public UserService(IUnitOfWork unitOfWork, IUserRepository userRepo)
        {
            _unitOfWork = unitOfWork;
            _userRepo = userRepo;
        }

        public User Delete(int id, out int affectedRow)
        {
            throw new NotImplementedException();
        }

        public User Get(string email)
        {
           User user = null;
            try
            {
                user = _userRepo.Find(a => a.Email == email).FirstOrDefault();
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
            }
            return user;
        }

        public void Get(int id)
        {
            throw new NotImplementedException();
        }

        public int Update(UpdateViewModel model, int Id)
        {
            throw new NotImplementedException();
        }
    }
}
