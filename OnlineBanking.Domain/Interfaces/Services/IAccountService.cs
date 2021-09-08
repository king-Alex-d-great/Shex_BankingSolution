

using OnlineBanking.Domain.Entities;
using OnlineBanking.Domain.Model;

namespace OnlineBanking.Domain.Interfaces.Services
{
    public interface IAccountService
    {
        Account Get(int userId);
        void checkBalance(User user);
        Account Register(RegisterViewModel model);
        int Update(UpdateViewModel model, int Id);
        Account Login(LoginViewModel model, out Account account);
        Account Delete(int id, out int affectedRow);
       
    }
}