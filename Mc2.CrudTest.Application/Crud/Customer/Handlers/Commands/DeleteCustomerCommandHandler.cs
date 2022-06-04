using AutoMapper;
using Mc2.CrudTest.Application.Exceptions;
using Mc2.CrudTest.Application.Crud.Customer.Requests.Commands;
using Mc2.CrudTest.Application.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Application.Crud.Customer.Handlers.Commands
{
    public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteCustomerCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await _unitOfWork.CustomerRepository.Get(request.Id);

            if (customer == null)
                throw new NotFoundException(nameof(Domain.Customer), request.Id);

            await _unitOfWork.CustomerRepository.Delete(customer);
            await _unitOfWork.Save();
            return Unit.Value;
        }
    }
}
