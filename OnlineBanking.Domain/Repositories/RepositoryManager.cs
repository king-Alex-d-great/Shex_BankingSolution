using OnlineBanking.Domain.Entities;
using OnlineBanking.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineBanking.Domain.Repositories
{
    public class RepositoryManager : IRepositoryManager
    {
        private AppDbContext _repositoryContext;
        private ICustomerRepository _customerRepository;

        public RepositoryManager(AppDbContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        public ICustomerRepository Customer
        {
            get
            {
                if (_customerRepository == null)
                    _customerRepository = new CustomerRepository(_repositoryContext);
                return _customerRepository;
            }
        }

        public void Save() => _repositoryContext.SaveChanges();
    }
}
