using AutoMapper;
using Mc2.CrudTest.AcceptanceTests.Mocks;
using Mc2.CrudTest.Application.Crud.Customer.Handlers.Commands;
using Mc2.CrudTest.Application.Crud.Customer.Requests.Commands;
using Mc2.CrudTest.Application.DTOs.Customer;
using Mc2.CrudTest.Application.Exceptions;
using Mc2.CrudTest.Application.Persistence;
using Mc2.CrudTest.Application.Profiles;
using Mc2.CrudTest.Application.Responses;
using Moq;
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
    public class DeleteCustomerCommandHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _mockUnit;
        private readonly CreateCustomerDto _customerDto;

        public DeleteCustomerCommandHandlerTests()
        {
            _mockUnit = MockMobileValidator.GetUnitOfWork();
            
            var mapperConfig = new MapperConfiguration(c => 
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
        }


        [Fact]
        public async Task Valid_Customer_Deleted_Test()
        {
            var customerId = 2;

            var handler = new DeleteCustomerCommandHandler(_mockUnit.Object, _mapper);
            var result = await handler.Handle(new DeleteCustomerCommand() { Id = customerId }, CancellationToken.None);

            result.ShouldBeOfType<MediatR.Unit>();
        }

        [Fact]
        public async Task InValid_Customer_Deleted_Test()
        {
            //Not found ID
            //----------------------------------------

            var customerId = 20;

            var handler = new DeleteCustomerCommandHandler(_mockUnit.Object, _mapper);
           
            try
            {
                var result = await handler.Handle(new DeleteCustomerCommand() { Id = customerId }, CancellationToken.None);
            }
            catch (Exception ex)
            {
                ex.ShouldBeOfType(typeof(NotFoundException));
            }

            var customers = await _mockUnit.Object.CustomerRepository.GetAll();
            customers.Count.ShouldBe(2);
        }
    }
}
