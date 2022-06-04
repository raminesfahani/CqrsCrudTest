using Mc2.CrudTest.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Application.Persistence
{
    public interface ICustomerRepository : IGenericRepository<Customer>
    {
        Task<bool> IsEmailUnique(string Email);
        Task<bool> IsInformationUnique(string Firstname, string Lastname, DateTime DateOfBirth);

        Task<bool> CanUpdateNewEmail(Customer customer, string NewEmail);
        Task<bool> CanUpdateNewInformation(Customer customer, string Firstname, string Lastname, DateTime DateOfBirth);
    }
}
