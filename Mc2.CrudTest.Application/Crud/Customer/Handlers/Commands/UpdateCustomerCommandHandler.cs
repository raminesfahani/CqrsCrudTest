using AutoMapper;
using Mc2.CrudTest.Application.DTOs.Customer.Validators;
using Mc2.CrudTest.Application.Exceptions;
using Mc2.CrudTest.Application.Crud.Customer.Requests.Commands;
using Mc2.CrudTest.Application.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Mc2.CrudTest.Application.DTOs.Customer;
using FluentValidation;

namespace Mc2.CrudTest.Application.Crud.Customer.Handlers.Commands
{
    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private CustomerValidator _validator;

        public UpdateCustomerCommandHandler(
            IUnitOfWork unitOfWork,
            CustomerValidator validator,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<Unit> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await _unitOfWork.CustomerRepository.Get(request.CustomerDto.Id);

            await ValidateRequest(request, customer, cancellationToken);
            return await UpdateCustomer(request, customer);
        }

        #region Functions

        private async Task<Unit> UpdateCustomer(UpdateCustomerCommand request, Domain.Customer customer)
        {
            _mapper.Map(request.CustomerDto, customer);

            await _unitOfWork.CustomerRepository.Update(customer);
            await _unitOfWork.Save();
            return Unit.Value;
        }

        private async Task ValidateRequest(UpdateCustomerCommand request, Domain.Customer customer, CancellationToken cancellationToken)
        {
            if (customer is null)
                throw new NotFoundException(nameof(Domain.Customer), request.CustomerDto.Id);

            var entity = _mapper.Map<CustomerDto>(request.CustomerDto);
            var validationResult = await _validator.ValidateAsync(entity, cancellationToken);

            if (validationResult.IsValid == false)
                throw new Exceptions.ValidationException(validationResult);
        } 
        #endregion
    }
}
