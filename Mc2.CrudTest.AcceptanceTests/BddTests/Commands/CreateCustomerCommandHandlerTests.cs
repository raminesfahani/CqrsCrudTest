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
    public class CreateCustomerCommandHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _mockUnit;
        private readonly IMobileValidator _mockMobileValidator;
        private readonly IBankAccountNumberValidator _mockBankAccountValidator;
        private readonly IEmailValidator _mockEmailValidator;
        private readonly IDuplicateCustomerValidator _mockDuplicateCustomerValidator;
        private CreateCustomerDto _customerDto;
        private CustomerValidator _validator;

        public CreateCustomerCommandHandlerTests()
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
            

            _customerDto = new CreateCustomerDto
            {
                Firstname = Helper.GetSaltString(),
                Lastname = Helper.GetSaltString(),
                Email = $"{Helper.GetSaltString()}@gmail.com",
                PhoneNumber = "+989120345399",
                DateOfBirth = new DateTime(1990, 11, 21),
                BankAccountNumber = "GB94BARC10201530093459",
            };
        }

        [Fact]
        public async Task Valid_Customer_Added_Test()
        {
            var handler = new CreateCustomerCommandHandler(_mockUnit.Object, _validator, _mapper);
            var result = await handler.Handle(new CreateCustomerCommand() { CustomerDto = _customerDto }, CancellationToken.None);

            var customers = await _mockUnit.Object.CustomerRepository.GetAll();

            result.ShouldBeOfType<BaseResponseObj<CreateCustomerDto>>();
            result.Result.ShouldBeEquivalentTo(_customerDto);

            customers.Count.ShouldBe(3);
        }

        [Fact]
        public async Task InValid_Customer_Added_Test()
        {
            //Duplicate Email
            //----------------------------------------
            _customerDto.Email = "r.esfahani@yahoo.com";

            var handler = new CreateCustomerCommandHandler(_mockUnit.Object, _validator, _mapper);
            var result = await handler.Handle(new CreateCustomerCommand() { CustomerDto = _customerDto }, CancellationToken.None);

            var customers = await _mockUnit.Object.CustomerRepository.GetAll();
            
            customers.Count.ShouldBe(2);

            result.ShouldBeOfType<BaseResponseObj<CreateCustomerDto>>();
            result.Success.ShouldBeFalse();

            //Invalid Email
            //----------------------------------------
            _customerDto.Email = "invalid email@";

            handler = new CreateCustomerCommandHandler(_mockUnit.Object, _validator, _mapper);
            result = await handler.Handle(new CreateCustomerCommand() { CustomerDto = _customerDto }, CancellationToken.None);

            customers = await _mockUnit.Object.CustomerRepository.GetAll();

            customers.Count.ShouldBe(2);

            result.ShouldBeOfType<BaseResponseObj<CreateCustomerDto>>();
            result.Success.ShouldBeFalse();


            //Invalid phone number
            //----------------------------------------
            _customerDto.PhoneNumber = "+9891247";
            _customerDto.Email = $"{Helper.GetSaltString()}@gmail.com";

            handler = new CreateCustomerCommandHandler(_mockUnit.Object, _validator, _mapper);
            result = await handler.Handle(new CreateCustomerCommand() { CustomerDto = _customerDto }, CancellationToken.None);

            customers = await _mockUnit.Object.CustomerRepository.GetAll();

            customers.Count.ShouldBe(2);

            result.ShouldBeOfType<BaseResponseObj<CreateCustomerDto>>();
            result.Success.ShouldBeFalse();

            //Duplicate Customer Info
            //----------------------------------------
            var existedCustomer = await _mockUnit.Object.CustomerRepository.Get(1);
            _customerDto = _mapper.Map<CreateCustomerDto>(existedCustomer);

            handler = new CreateCustomerCommandHandler(_mockUnit.Object, _validator, _mapper);
            result = await handler.Handle(new CreateCustomerCommand() { CustomerDto = _customerDto }, CancellationToken.None);

            customers = await _mockUnit.Object.CustomerRepository.GetAll();

            customers.Count.ShouldBe(2);

            result.ShouldBeOfType<BaseResponseObj<CreateCustomerDto>>();
            result.Success.ShouldBeFalse();
        }
    }
}
