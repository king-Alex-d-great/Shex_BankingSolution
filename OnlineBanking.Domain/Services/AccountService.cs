using System;
using System.Linq;
using System.Security.Cryptography;
using OnlineBanking.Domain.Entities;
using OnlineBanking.Domain.Interfaces.Services;
using OnlineBanking.Domain.Model;
using OnlineBanking.Domain.Encryption;
using OnlineBanking.Domain.UnitOfWork;
using System.Text;

namespace OnlineBanking.Domain.Services
{
    public class AccountService : IAccountService
    {
        private readonly UnitOfWork<Account> _unitOfWork;
        public AccountService(UnitOfWork<Account> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void checkBalance(User user)
        {
            throw new NotImplementedException();
        }
        public Account Login(LoginViewModel model, out Account account)
        {
            bool isValidated = false;
            Console.ForegroundColor = ConsoleColor.White;

            model.UsernameEmail = model.UsernameEmail.Trim().ToLower();
            model.Password = model.Password.Trim().ToLower();
            isValidated = AuthenticateUser(model.UsernameEmail, model.Password, out Account account1);
            // account = _unitOfWork.Accounts.Find(a => a.Id == user.AccountId).FirstOrDefault();
            if (!isValidated)
            {
                account = null;
                Console.WriteLine("Invalid Username or password\n");
                return null;
            }
            account = _unitOfWork._entity.Find(a => a.Id == account1.Id).FirstOrDefault();
            Console.WriteLine(" login Successful\n\n");
            Console.WriteLine(account.Balance);
            return account;
        }

        public Account Delete(int id, out int affectedRow)
        {
            affectedRow = 0;
            try
            {
                var result = _unitOfWork._entity.Get(id);
                _unitOfWork._entity.Delete(result);
                affectedRow = _unitOfWork.Commit();
                Console.WriteLine($"Account : {id} deleted\n\n");
                return result;
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
            }
            return null;
        }

        public Account Get(int id)
        {
            Account account = null;
            try
            {
                account = _unitOfWork._entity.Get(id);
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
            }
            return account;
        }


        public Account Register(RegisterViewModel model)
        {
            Account account = null;
            /*try
            {*/
            var salt = PasswordHasher.GetSalt();

                if (!Validate(model))
                {
                    return null ;
                }

                account = new Account
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    IsActive = model.IsActive,
                    Password = PasswordHasher.HashPassword(Encoding.UTF8.GetBytes(model.ConfirmPassword), Encoding.UTF8.GetBytes(salt)),
                    AccountNumber = (RandomNumberGenerator.GetInt32(1000, 9999) * RandomNumberGenerator.GetInt32(10000, 99999)).ToString(),
                    AccountType = model.AccountType,
                    CreatedAt = DateTime.Now,
                    CreatedBy = "Shola nejo",
                    Customer = new Customer(),
                    Balance = model.AccountType == Enumerators.AccountType.Savings ? 5000 : 0,
                };
                _unitOfWork.Entity.Add(account);
                _unitOfWork.Commit();
                Console.WriteLine("Success!\n");
           
            /*catch (Exception error)
            {
                Console.WriteLine(error.Message);
                Console.WriteLine(error.InnerException);
            }*/
            return account;
        }


        public int Update(UpdateViewModel model, int Id)
        {
            int affectedRow = 0;
            try
            {
                var account = _unitOfWork._entity.Get(Id);
                //var salt = user.Salt;
                account.Email = model.Email ??= account.Email;
                account.AccountType = model.AccountType ??= account.AccountType;
                account.UpdatedAt = DateTime.Now;
                account.UpdatedBy = "King Alex";
                // var currenthashedpassword = Hasher.hashPassword(Encoding.UTF8.GetBytes(model.CurrentPassword), Encoding.UTF8.GetBytes(salt));

                /*if (currenthashedpassword == user.Password && !string.IsNullOrWhiteSpace(model.CurrentPassword))
                {

                    if (model.NewPassword == model.ConfirmNewPassword && !string.IsNullOrWhiteSpace(model.NewPassword))
                    {
                        user.Password = Hasher.hashPassword(Encoding.UTF8.GetBytes(model.ConfirmNewPassword), Encoding.UTF8.GetBytes(salt));
                        affectedRow = _unitOfWork.Commit();
                        Console.WriteLine("User Updated successfully\n\n");
                        return affectedRow;
                    }
                    Console.WriteLine("Confirm password field did not match newpassword field ");
                }*/
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
            var error = string.IsNullOrWhiteSpace(model.Email) ? ErrorMessage("Email", out int count) :
            string.IsNullOrWhiteSpace(model.FirstName) ? ErrorMessage("FirstName", out count) :
            string.IsNullOrWhiteSpace(model.LastName) ? ErrorMessage("LastName", out count) :
            string.IsNullOrWhiteSpace(model.Password) ? ErrorMessage("Password", out count) : string.Empty;

            if (string.IsNullOrEmpty(error))
            {
                return true;
            }
            return false;
        }
        public static string ErrorMessage(string name, out int count)
        {
            count = 0;
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
                return string.Empty;
            }
            return "yoo";
        }

        public bool AuthenticateUser(string userNameEmail, string password, out Account account)
        {
            account = null;
            var isValidated = false;

            account = _unitOfWork._entity.Find(a => a.Email == userNameEmail).FirstOrDefault();

            if (account == null)
                return false;

            // var salt = user.Salt;
            // var saltedLoginPassword = Hasher.hashPassword(Encoding.UTF8.GetBytes(password), Encoding.UTF8.GetBytes(salt));

            // if (saltedLoginPassword == user.Password)
            if (password == account.ConfirmPassword)
            {
                isValidated = true;
                return isValidated;
            }
            Console.WriteLine("Your username or password is Incorrect\nIf you are a new user , please click 1 to register\n");
            return isValidated;
        }


    }
}
