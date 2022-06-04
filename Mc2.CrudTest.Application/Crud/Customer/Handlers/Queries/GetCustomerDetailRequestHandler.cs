using AutoMapper;
using Mc2.CrudTest.Application.DTOs;
using Mc2.CrudTest.Application.DTOs.Customer;
using Mc2.CrudTest.Application.Crud.Customer.Requests.Queries;
using Mc2.CrudTest.Application.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Mc2.CrudTest.Application.Exceptions;
using Mc2.CrudTest.Application.Responses;

namespace Mc2.CrudTest.Application.Crud.Customer.Handlers.Queries
{
    public class GetCustomerDetailRequestHandler : IRequestHandler<GetCustomerDetailRequest, BaseResponseObj<CustomerDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetCustomerDetailRequestHandler(
           IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<BaseResponseObj<CustomerDto>> Handle(GetCustomerDetailRequest request, CancellationToken cancellationToken)
        {
            var customer = await _unitOfWork.CustomerRepository.Get(request.Id);
            if (customer is null)
                throw new NotFoundException(nameof(Domain.Customer), request.Id);

            return new BaseResponseObj<CustomerDto>
            {
                Result = _mapper.Map<CustomerDto>(customer),
            };
        }
    }
}
