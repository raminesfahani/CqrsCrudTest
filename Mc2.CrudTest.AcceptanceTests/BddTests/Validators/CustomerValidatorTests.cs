using AutoMapper;
using Mc2.CrudTest.AcceptanceTests.Infrastructure;
using Mc2.CrudTest.AcceptanceTests.Mocks;
using Mc2.CrudTest.Application.Crud.Customer.Handlers.Commands;
using Mc2.CrudTest.Application.Crud.Customer.Requests.Commands;
using Mc2.CrudTest.Application.DTOs.Customer;
using Mc2.CrudTest.Application.DTOs.Customer.Validators;
using Mc2.CrudTest.Application.DTOs.Customer.Validators.Common.EmailValidator;
using Mc2.CrudTest.Application.Persistence;
using Mc2.CrudTest.Application.Profiles;
using Mc2.CrudTest.Application.Responses;
using Moq;
using PhoneNumbers;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Mc2.CrudTest.AcceptanceTests.Customer.Commands
{
    public class CustomerValidatorTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _mockUnit;
        private readonly IMobileValidator _mockMobileValidator;
        private readonly IBankAccountNumberValidator _mockBankAccountValidator;
        private readonly IEmailValidator _mockEmailValidator;
        private readonly IDuplicateCustomerValidator _mockDuplicateCustomerValidator;
        private CustomerValidator _validator;

        public CustomerValidatorTests()
        {
            _mockUnit = MockUnitOfWork.GetUnitOfWork();
            _mockMobileValidator = new MobileValidator();
            _mockBankAccountValidator = new BankAccountNumberValidator();
            _mockEmailValidator = new EmailValidator();
            _mockDuplicateCustomerValidator = new DuplicateCustomerValidator(_mockUnit.Object.CustomerRepository);

            _validator = new CustomerValidator(_mockBankAccountValidator, _mockMobileValidator, _mockDuplicateCustomerValidator, _mockEmailValidator, _mapper);
            
            var mapperConfig = new MapperConfiguration(c => 
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
        }

        [Theory]
        [InlineData("+9821887766554123", false)]
        [InlineData("+98 912 034 5399", true)]
        public void ValidateMobile(string Mobile, bool Expectation)
        {
            var result = _mockMobileValidator.Validate(Mobile);
            Assert.Equal(result, Expectation);
        }

        [Theory]
        [InlineData("r.esfahani", false)]
        [InlineData("r@", false)]
        [InlineData("r.esfahani@yahoo.com", true)]
        public void ValidateEmail(string Mobile, bool Expectation)
        {
            var result = _mockEmailValidator.Validate(Mobile);
            Assert.Equal(result, Expectation);
        }

        [Theory]
        [InlineData("GB96BARC202015300934591", false)]
        [InlineData("GB94BARC20201530093459", false)]
        [InlineData("GB94BARC10201530093459", true)]
        public void ValidateBankAccountNumber(string Mobile, bool Expectation)
        {
            var result = _mockBankAccountValidator.Validate(Mobile);
            Assert.Equal(result, Expectation);
        }
    }
}
