using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineBanking.Domain.Interfaces.Repositories
{
    public interface IRepositoryManager
    {
        ICustomerRepository Customer { get; }
        void Save();
    }
}
