using AutoMapper;
using Mc2.CrudTest.Application.Crud.Customer.Requests.Queries;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Mc2.CrudTest.Application.Persistence;
using Mc2.CrudTest.Application.DTOs.Customer;
using Mc2.CrudTest.Application.Responses;

namespace Mc2.CrudTest.Application.Crud.Customer.Handlers.Queries
{
    public class GetCustomerListRequestHandler : IRequestHandler<GetCustomerListRequest, BaseResponseObj<List<CustomerDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetCustomerListRequestHandler(
           IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponseObj<List<CustomerDto>>> Handle(GetCustomerListRequest request, CancellationToken cancellationToken)
        {
            var customers = await _unitOfWork.CustomerRepository.GetAll();
            var customersDto = _mapper.Map<List<CustomerDto>>(customers);

            return new BaseResponseObj<List<CustomerDto>>
            {
                Result = customersDto,
            };
        }
    }
}
