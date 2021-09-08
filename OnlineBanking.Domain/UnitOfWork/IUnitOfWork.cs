using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using OnlineBanking.Domain.Entities;
using OnlineBanking.Domain.Interfaces.Repositories;

namespace OnlineBanking.Domain.UnitOfWork
{
    public interface IUnitOfWork<Tentity> where Tentity : class
    {        
        IRepository<Tentity> _entity { get; }        
        Task<int> CommitAsync();
        int Commit();
    }
}
