using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using OnlineBanking.Domain.Interfaces.Services;

using OnlineBanking.Domain.Entities;
using OnlineBanking.Domain.Interfaces.Repositories;
using OnlineBanking.Domain.Model;
using OnlineBanking.Domain.UnitOfWork;

namespace OnlineBanking.Domain.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAccountRepository _accountRepo;        
        public AccountService(IUnitOfWork unitOfWork, IAccountRepository accountRepo)
        {
            _unitOfWork = unitOfWork;
            _accountRepo = accountRepo;
        }
        
        

        public (int affectedRow, Account account) Delete(int id)
        {
            int affectedRow = 0;
            Account account = null;
            try
            {
                account = _accountRepo.Get(id);
                _accountRepo.Delete(account);
                affectedRow = _unitOfWork.Commit();
                Console.WriteLine($"Account : {id} deleted\n\n");
                return (affectedRow, account);
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
            }
            return (affectedRow, account);
        }

        /*public Account Get(int id)
        {
            Account account = null;
            try
            {
                account = _accountRepo.Get(id);

            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
            }
            return account;
        }
*/

        public Account Get(string Id)
        {
           Account user = null;
            try
            {
                user = _accountRepo.Find(a => (a.UserId).ToString() == Id).FirstOrDefault();
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
            }
            return user;
        }
        

        /* public int Update(UpdateViewModel model, int Id)
         {
             int affectedRow = 0;
             try
             {
                 var account = _accountRepo.Get(Id);
                 //var salt = user.Salt;
                 account.Email = model.Email ??= account.Email;
                 account.AccountType = model.AccountType ??= account.AccountType;
                 account.UpdatedAt = DateTime.Now;
                 account.UpdatedBy = "King Alex";
                 // var currenthashedpassword = Hasher.hashPassword(Encoding.UTF8.GetBytes(model.CurrentPassword), Encoding.UTF8.GetBytes(salt));

                 *//*if (currenthashedpassword == user.Password && !string.IsNullOrWhiteSpace(model.CurrentPassword))
                 {

                     if (model.NewPassword == model.ConfirmNewPassword && !string.IsNullOrWhiteSpace(model.NewPassword))
                     {
                         user.Password = Hasher.hashPassword(Encoding.UTF8.GetBytes(model.ConfirmNewPassword), Encoding.UTF8.GetBytes(salt));
                         affectedRow = _unitOfWork.Commit();
                         Console.WriteLine("User Updated successfully\n\n");
                         return affectedRow;
                     }
                     Console.WriteLine("Confirm password field did not match newpassword field ");
                 }*//*
                 return affectedRow;
             }
             catch (Exception error)
             {
                 Console.WriteLine(error.Message);
             }
             return 0;
         }

         private bool Validate(RegisterViewModel model)
         {
             (String error, int count) error = string.IsNullOrWhiteSpace(model.Email) ? ErrorMessage("Email") :
             string.IsNullOrWhiteSpace(model.FirstName) ? ErrorMessage("FirstName") :
             string.IsNullOrWhiteSpace(model.LastName) ? ErrorMessage("LastName") :
             string.IsNullOrWhiteSpace(model.Password) ? ErrorMessage("Password") : (string.Empty, 2);

             if (string.IsNullOrEmpty(error.error))
             {
                 return true;
             }
             return false;
         }
         public static (string err, int count) ErrorMessage(string name)
         {
             var count = 0;
             while (true && count < 2)
             {
                 Console.ForegroundColor = ConsoleColor.Yellow;
                 Console.WriteLine("\nError Alert");
                 Console.ForegroundColor = ConsoleColor.Red;
                 Console.WriteLine($" Your {name} cannot be empty\n");
                 Console.ForegroundColor = ConsoleColor.White;
                 Console.WriteLine($"You have {2 - count} more chances\n");
                 Console.WriteLine($"Please Input your {name}\n");
                 var Input = Console.ReadLine();
                 if (!string.IsNullOrWhiteSpace(Input))
                 {
                     break;
                 }
                 count++;
             }
             if (count >= 2)
             {
                 Console.WriteLine("You've Exhausted your chances. ");
                 return (string.Empty, count);
             }
             return ("No error", count);
         }

         public (bool isValidated, Account account) AuthenticateUser(string userNameEmail, string password)
         {
             var isValid = false;
             var account = _accountRepo.Find(a => a.Email == userNameEmail).FirstOrDefault();
             if (account == null)
                 return (isValidated: isValid, account: account);

             //var salt = user.Salt;
             // var saltedLoginPassword = Hasher.hashPassword(Encoding.UTF8.GetBytes(password), Encoding.UTF8.GetBytes(salt));
             // if (saltedLoginPassword == user.Password)

             if (password == account.ConfirmPassword)
             {
                 isValid = true;
             }
             Console.WriteLine("Your username or password is Incorrect\nIf you are a new user , please click 1 to register\n");
             return (isValidated: isValid, account: account);
         }
 */
        public void checkBalance(User user)
        {
            throw new NotImplementedException();
        }

        public Account Register(RegisterViewModel model)
        {
            throw new NotImplementedException();
        }

        public int Update(UpdateViewModel model, int Id)
        {
            throw new NotImplementedException();
        }

        public Account Login(LoginViewModel model)
        {
            throw new NotImplementedException();
        }

        public Account Get(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
