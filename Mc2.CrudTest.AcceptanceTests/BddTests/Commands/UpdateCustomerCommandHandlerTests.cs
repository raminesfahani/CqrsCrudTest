using AutoMapper;
using Mc2.CrudTest.AcceptanceTests.Infrastructure;
using Mc2.CrudTest.AcceptanceTests.Mocks;
using Mc2.CrudTest.Application.Crud.Customer.Handlers.Commands;
using Mc2.CrudTest.Application.Crud.Customer.Requests.Commands;
using Mc2.CrudTest.Application.DTOs.Customer;
using Mc2.CrudTest.Application.DTOs.Customer.Validators;
using Mc2.CrudTest.Application.Exceptions;
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

namespace Mc2.CrudTest.AcceptanceTests.BddTests.Commands
{
    public class UpdateCustomerCommandHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _mockUnit;
        private readonly Mock<IMobileValidator> _mockMobileValidator;
        private UpdateCustomerDto _customerDto;
        private CustomerValidator _validator;

        public UpdateCustomerCommandHandlerTests()
        {
            _mockUnit = MockUnitOfWork.GetUnitOfWork();
            _mockMobileValidator = MockMobileValidator.GetMobileValidator();
            _validator = new CustomerValidator(_mockUnit.Object.CustomerRepository);

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
        }

        [Fact]
        public async Task Valid_Customer_Updated_Test()
        {
            var customer = await _mockUnit.Object.CustomerRepository.Get(1);
            _customerDto = _mapper.Map<UpdateCustomerDto>(customer);

            _customerDto.Email = "ramin.esfahani@yahoo.com";

            var handler = new UpdateCustomerCommandHandler(_mockUnit.Object, _validator, _mapper);
            var result = await handler.Handle(new UpdateCustomerCommand() { CustomerDto = _customerDto }, CancellationToken.None);
            customer = await _mockUnit.Object.CustomerRepository.Get(1);

            result.ShouldBeOfType<MediatR.Unit>();
            customer.Email.ShouldBeEquivalentTo(_customerDto.Email);
        }


        [Fact]
        public async Task InValid_Customer_Updated_Test()
        {
            //Duplicate Email
            //----------------------------------------
            var customer = await _mockUnit.Object.CustomerRepository.Get(1);
            _customerDto = _mapper.Map<UpdateCustomerDto>(customer);

            _customerDto.Email = "r.esfahani@yahoo.com";

            var handler = new UpdateCustomerCommandHandler(_mockUnit.Object, _validator, _mapper);

            try
            {
                var result = await handler.Handle(new UpdateCustomerCommand() { CustomerDto = _customerDto }, CancellationToken.None);
            }
            catch (Exception ex)
            {
                ex.ShouldBeOfType<ValidationException>();
            }


            //Invalid phone number
            //----------------------------------------
            _customerDto.PhoneNumber = "+989120";
            _customerDto.Email = $"{Helper.GetSaltString()}@gmail.com";

            handler = new UpdateCustomerCommandHandler(_mockUnit.Object, _validator, _mapper);
            try
            {
                var result = await handler.Handle(new UpdateCustomerCommand() { CustomerDto = _customerDto }, CancellationToken.None);
            }
            catch (Exception ex)
            {
                ex.ShouldBeOfType(typeof(ValidationException));
            }


            //Not found customer
            //----------------------------------------
            var existedCustomer = await _mockUnit.Object.CustomerRepository.Get(1);
            _customerDto = _mapper.Map<UpdateCustomerDto>(existedCustomer);
            _customerDto.Id = 10;

            handler = new UpdateCustomerCommandHandler(_mockUnit.Object, _validator, _mapper);
            try
            {
                var result = await handler.Handle(new UpdateCustomerCommand() { CustomerDto = _customerDto }, CancellationToken.None);
            }
            catch (Exception ex)
            {
                ex.ShouldBeOfType<NotFoundException>();
            }
        }
    }
}
