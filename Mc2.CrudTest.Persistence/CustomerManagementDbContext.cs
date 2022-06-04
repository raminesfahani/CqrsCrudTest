using Mc2.CrudTest.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Persistence
{
    public class CustomerManagementDbContext : DbContext
    {
        public CustomerManagementDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CustomerManagementDbContext).Assembly);
        }

        public DbSet<Customer> Customers { get; set; }
    }
}
