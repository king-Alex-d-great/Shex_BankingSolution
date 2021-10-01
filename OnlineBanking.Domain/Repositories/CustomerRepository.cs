using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using OnlineBanking.Domain.Entities;
using OnlineBanking.Domain.Interfaces.Repositories;

namespace OnlineBanking.Domain.Repositories
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
       

        public CustomerRepository(DbContext context) : base(context)
        {
           
        }

        public IEnumerable<Customer> GetAllCustomer() =>
            GetAll()
            .OrderBy(c => c.Id)
            .ToList();
    }
}
