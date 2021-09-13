using OnlineBanking.Domain.Entities;
using OnlineBanking.Domain.Model;

namespace OnlineBanking.Domain.Interfaces.Services
{
    public interface IUserService
    {
        public User Get(string email);        
        int Update(UpdateViewModel model, int Id);        
        User Delete(int id, out int affectedRow);
        void Get(int id);
    }
}
