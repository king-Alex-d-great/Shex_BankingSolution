using OnlineBanking.Domain.Entities;
using OnlineBanking.Domain.Interfaces.Repositories;
using OnlineBanking.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBanking.Domain.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        private IRepository<Customer> _customers;
        private IRepository<Account> _accounts;
        private IRepository<User> _users;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public IRepository<Account> Accounts { get { return _accounts ??= _accounts = new AccountRepository(_context); } }

        public IRepository<User> Users { get { return _users ??= _users = new UserRepository(_context); } }

        public IRepository<Customer> Customers { get { return _customers ??= _customers = new CustomerRepository(_context); } }

        public int Commit()
        {
            return _context.SaveChanges();
        }

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
