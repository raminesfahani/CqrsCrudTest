
using Mc2.CrudTest.Application.Persistence;
using Mc2.CrudTest.Domain;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mc2.CrudTest.AcceptanceTests.Mocks
{
    public static class MockCustomerRepository
    {
        public static Mock<ICustomerRepository> GetCustomerRepository()
        {
            var customers = new List<Domain.Customer>
            {
                new Domain.Customer {
                    Id = 1,
                    Firstname = "Test",
                    Lastname = "Test",
                    Email ="test@yahoo.com",
                    DateCreated = DateTime.Now,
                    LastModifiedDate = DateTime.Now,
                    PhoneNumber = 989120345399,
                    DateOfBirth = new DateTime(1990,11,21),
                    BankAccountNumber = "GB94BARC10201530093459"
                },
                new Domain.Customer {
                    Id = 2,
                    Firstname = "Ramin",
                    Lastname = "Esfahani",
                    Email ="r.esfahani@yahoo.com",
                    DateCreated = DateTime.Now,
                    LastModifiedDate = DateTime.Now,
                    PhoneNumber = 989120345399,
                    DateOfBirth = new DateTime(1990,11,21),
                    BankAccountNumber = "GB94BARC10201530093459"
                }
            };

            var mockRepo = new Mock<ICustomerRepository>();

            mockRepo.Setup(r => r.GetAll()).ReturnsAsync(customers);
            

            mockRepo.Setup(r => r.IsEmailUnique(It.IsAny<string>())).ReturnsAsync((string email) =>
            {
                return customers.Any(c => c.Email.ToUpper() == email.ToUpper()) == false;
            });
            mockRepo.Setup(r => r.IsInformationUnique(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>())).ReturnsAsync((string firstname, string lastname, DateTime birthdate) =>
            {
                return customers.Any(c => c.Firstname.ToUpper() == firstname.ToUpper() &&
                                                    c.Lastname.ToUpper() == lastname.ToUpper() &&
                                                    c.DateOfBirth == birthdate) == false;
            });

            mockRepo.Setup(r => r.CanUpdateNewEmail(It.IsAny<Domain.Customer>(), It.IsAny<string>())).ReturnsAsync((Domain.Customer customer, string newEmail) =>
            {
                return customers.Any(x => x.Id != customer.Id && x.Email.ToUpper() == newEmail.ToUpper()) == false;
            });
            mockRepo.Setup(r => r.CanUpdateNewInformation(It.IsAny<Domain.Customer>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>())).ReturnsAsync((Domain.Customer customer, string firstname, string lastname, DateTime birthdate) =>
            {
                return customers.Any(x => x.Id != customer.Id &&
                                                            x.Firstname.ToUpper() == firstname.ToUpper() &&
                                                            x.Lastname.ToUpper() == lastname.ToUpper() &&
                                                            x.DateOfBirth == birthdate) == false;
            });

            mockRepo.Setup(r => r.Get(It.IsAny<int>())).ReturnsAsync((int id) =>
            {
                var customer = customers.FirstOrDefault(x=>x.Id == id);
                return customer;
            });

            mockRepo.Setup(r => r.Exists(It.IsAny<int>())).ReturnsAsync((int id) =>
            {
                return customers.Any(x => x.Id == id);
            });

            mockRepo.Setup(r => r.Add(It.IsAny<Domain.Customer>())).ReturnsAsync((Domain.Customer customer) => 
            {
                customers.Add(customer);
                return customer;
            });

            return mockRepo;

        }
    }
}
