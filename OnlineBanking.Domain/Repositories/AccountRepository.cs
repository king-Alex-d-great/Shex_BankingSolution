using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using OnlineBanking.Domain.Entities;
using OnlineBanking.Domain.Interfaces.Repositories;

namespace OnlineBanking.Domain.Repositories
{
    public class AccountRepository : Repository<Account>, IAccountRepository
    {

        public AccountRepository(DbContext context ):base(context)
        {
           
        }
    }
}
