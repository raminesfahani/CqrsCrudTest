using Mc2.CrudTest.Application.DTOs.Customer.Validators;
using Mc2.CrudTest.Application.DTOs.Customer.Validators.Common.EmailValidator;
using Mc2.CrudTest.Application.Persistence;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mc2.CrudTest.AcceptanceTests.Mocks
{
    public static class MockValidator
    {
        public static Mock<IMobileValidator> GetMobileValidator()
        {
            var mockUow = new Mock<IMobileValidator>();
            //mockUow.Setup(x=>x.Validate(It.IsAny<string>())).Returns();
            return mockUow;
        }

        public static Mock<IBankAccountNumberValidator> GetBankAccountValidator()
        {
            var mockUow = new Mock<IBankAccountNumberValidator>();
            return mockUow;
        }

        public static Mock<IEmailValidator> GetEmailValidator()
        {
            var mockUow = new Mock<IEmailValidator>();
            return mockUow;
        }

        public static Mock<IDuplicateCustomerValidator> GetDuplicateValidator()
        {
            var mockUow = new Mock<IDuplicateCustomerValidator>();
            return mockUow;
        }
    }
}
