using OnlineBanking.Domain.Entities;
using WebUI.domain.Models;

namespace WebUI.domain.Interfaces.Services
{
   public interface ICustomerService
   {
      int? Add(Customer customer);
   }
}
