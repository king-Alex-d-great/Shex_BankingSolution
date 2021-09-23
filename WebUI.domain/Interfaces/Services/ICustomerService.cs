using System;
using OnlineBanking.Domain.Entities;
using WebUI.domain.Model;

namespace WebUI.domain.Interfaces.Services
{
    public interface ICustomerService
    {
        int? Add(Customer customer);
        public (bool isAgeMoreThanMinimumAge, bool isAgeLessThanMaximumAge, int AffectedRows) Add (EnrollCustomerViewModel enrollModel);
        public Customer GetCustomer(string userId);
        public Customer GetCustomerWithAccount(string accountId);

    }
}
