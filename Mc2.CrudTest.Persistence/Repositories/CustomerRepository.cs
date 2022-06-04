using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mc2.CrudTest.Application.Persistence;
using Mc2.CrudTest.Domain;
using Microsoft.EntityFrameworkCore;

namespace Mc2.CrudTest.Persistence.Repositories
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        private readonly CustomerManagementDbContext _dbContext;

        public CustomerRepository(CustomerManagementDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> CanUpdateNewEmail(Customer customer, string NewEmail)
        {
            return (await _dbContext.Customers.AnyAsync(x => x.Id != customer.Id && x.Email.ToUpper() == NewEmail.ToUpper())) == false;
        }

        public async Task<bool> CanUpdateNewInformation(Customer customer, string Firstname, string Lastname, DateTime DateOfBirth)
        {
            return (await _dbContext.Customers.AnyAsync(x => x.Id != customer.Id && 
                                                            x.Firstname.ToUpper() == Firstname.ToUpper() && 
                                                            x.Lastname.ToUpper() == Lastname.ToUpper() &&  
                                                            x.DateOfBirth == DateOfBirth)
                                                            ) == false;
        }

        public async Task<bool> IsEmailUnique(string Email)
        {
            return (await _dbContext.Customers.AnyAsync(c => c.Email.ToUpper() == Email.ToUpper())) == false;
        }

        public async Task<bool> IsInformationUnique(string Firstname, string Lastname, DateTime DateOfBirth)
        {
            return (await _dbContext.Customers.AnyAsync(c => c.Firstname.ToUpper() == Firstname.ToUpper() && 
                                                    c.Lastname.ToUpper() == Lastname.ToUpper() && 
                                                    c.DateOfBirth == DateOfBirth)) == false;
        }
    }
}
 