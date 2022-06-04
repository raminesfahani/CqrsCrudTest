using AutoMapper;
using Mc2.CrudTest.AcceptanceTests.Mocks;
using Mc2.CrudTest.API.Controllers;
using Mc2.CrudTest.Application.Crud.Customer.Handlers.Queries;
using Mc2.CrudTest.Application.Crud.Customer.Requests.Queries;
using Mc2.CrudTest.Application.DTOs.Customer;
using Mc2.CrudTest.Application.Exceptions;
using Mc2.CrudTest.Application.Persistence;
using Mc2.CrudTest.Application.Profiles;
using Mc2.CrudTest.Application.Responses;
using Mc2.CrudTest.Persistence;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Mc2.CrudTest.AcceptanceTests.Customer.Queries
{
    public class GetCustomerListRequestHandlerTests
    {
        private readonly IMapper _mapper;
        private Mock<IUnitOfWork> _mockUnit;
        public GetCustomerListRequestHandlerTests()
        {
            _mockUnit = MockUnitOfWork.GetUnitOfWork();
            var mapperConfig = new MapperConfiguration(c => 
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
        }

        [Fact]
        public async Task Valid_GetCustomerList_Test()
        {
            var handler = new GetCustomerListRequestHandler(_mockUnit.Object, _mapper);

            var result = await handler.Handle(new GetCustomerListRequest(), CancellationToken.None);

            result.ShouldBeOfType<BaseResponseObj<List<CustomerDto>>>();
            result.Result.Count.ShouldBe(2);
        }

        [Fact]
        public async Task Valid_GetCustomerDetail_Test()
        {
            var customerId = 1;

            var handler = new GetCustomerDetailRequestHandler(_mockUnit.Object, _mapper);

            var result = await handler.Handle(new GetCustomerDetailRequest() { Id = customerId }, CancellationToken.None);

            result.ShouldBeOfType<BaseResponseObj<CustomerDto>>();

            result.Success.ShouldBeTrue();
            result.Result.Email.ShouldBeEquivalentTo("test@yahoo.com");
        }

        [Fact]
        public async Task Invalid_GetCustomerDetail_Test()
        {
            var customerId = 101;

            var handler = new GetCustomerDetailRequestHandler(_mockUnit.Object, _mapper);

            try
            {
                var result = await handler.Handle(new GetCustomerDetailRequest() { Id = customerId }, CancellationToken.None);
            }
            catch (Exception ex)
            {
                ex.ShouldBeOfType(typeof(NotFoundException));
            }
        }
    }
}
