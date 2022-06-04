
using Mc2.CrudTest.Application.Persistence;
using Mc2.CrudTest.Domain;
using Mc2.CrudTest.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly CustomerManagementDbContext _context;
        private ICustomerRepository _customerRepository;


        public UnitOfWork(CustomerManagementDbContext context)
        {
            _context = context;
        }

        public ICustomerRepository CustomerRepository =>
            _customerRepository ??= new CustomerRepository(_context);
        
        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task Save() 
        {

            //Automatic updating or setting date of creation or modifying records in database
            //-------------------------------------------------------------------------------

            foreach (var entry in _context.ChangeTracker.Entries<BaseDomainEntity>()
                                          .Where(q => q.State == EntityState.Added || q.State == EntityState.Modified))
            {
                entry.Entity.LastModifiedDate = DateTime.Now;

                if (entry.State == EntityState.Added)
                {
                    entry.Entity.DateCreated = DateTime.Now;
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}
