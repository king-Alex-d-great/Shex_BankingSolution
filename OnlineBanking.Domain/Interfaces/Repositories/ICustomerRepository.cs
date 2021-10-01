using System;
using System.Collections.Generic;
using System.Text;
using OnlineBanking.Domain.Entities;

namespace OnlineBanking.Domain.Interfaces.Repositories
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        IEnumerable<Customer> GetAllCustomer();
    }
}
