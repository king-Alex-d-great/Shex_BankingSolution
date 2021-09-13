using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using OnlineBanking.Domain.Entities;
using OnlineBanking.Domain.Interfaces.Repositories;
using OnlineBanking.Domain.Repositories;

namespace OnlineBanking.Domain.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;   
       // public IRepository<Tentity> _entity { get; set; }
        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }        
       // public  IRepository<Tentity> Entity { get { return _entity ??= _entity = new Repository<Tentity>(_context); } }       

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