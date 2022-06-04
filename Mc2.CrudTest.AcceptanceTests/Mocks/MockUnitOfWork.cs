using Mc2.CrudTest.Application.Persistence;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mc2.CrudTest.AcceptanceTests.Mocks
{
    public static class MockUnitOfWork
    {
        public static Mock<IUnitOfWork> GetUnitOfWork()
        {
            var mockUow = new Mock<IUnitOfWork>();
            var mockCustomerRepo = MockCustomerRepository.GetCustomerRepository();

            mockUow.Setup(r => r.CustomerRepository).Returns(mockCustomerRepo.Object);

            return mockUow;
        }
    }
}
