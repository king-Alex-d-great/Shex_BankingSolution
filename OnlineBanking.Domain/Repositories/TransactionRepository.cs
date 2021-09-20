using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using OnlineBanking.Domain.Entities;
using OnlineBanking.Domain.Interfaces.Repositories;

namespace OnlineBanking.Domain.Repositories
{
    public class TransactionRepository : Repository<Transaction>,ITransactionRepository
    {
      
            public TransactionRepository(DbContext context) : base(context)
            {

            }
        
    }
}
