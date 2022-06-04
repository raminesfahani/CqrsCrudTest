using AutoMapper;
using Mc2.CrudTest.Application.Exceptions;
using Mc2.CrudTest.Application.Crud.Customer.Requests.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Mc2.CrudTest.Application.Responses;
using System.Linq;
using Mc2.CrudTest.Application.Persistence;
using Mc2.CrudTest.Application.DTOs.Customer.Validators;
using Mc2.CrudTest.Application.DTOs.Customer;
using FluentValidation.Results;
using FluentValidation;

namespace Mc2.CrudTest.Application.Crud.Customer.Handlers.Commands
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, BaseResponseObj<CreateCustomerDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly CustomerValidator _validator;

        public CreateCustomerCommandHandler(
           IUnitOfWork unitOfWork,
           CustomerValidator validator,
            IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<BaseResponseObj<CreateCustomerDto>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponseObj<CreateCustomerDto>();
            var customer = _mapper.Map<CustomerDto>(request.CustomerDto);

            var validationResult = await _validator.ValidateAsync(customer);

            if (validationResult.IsValid == false)
            {
                response = await InvalidResponse(response, validationResult);
            }
            else
            {
                await CreateCustomer(request);
                response = await SuccessResponse(request, response);
            }

            return response;
        }

        #region Functions
        private async Task<BaseResponseObj<CreateCustomerDto>> SuccessResponse(CreateCustomerCommand request, BaseResponseObj<CreateCustomerDto> response)
        {
            response.Success = true;
            response.Result = request.CustomerDto;
            response.Message = "Customer has been added successfully.";
            return response;
        }

        private async Task CreateCustomer(CreateCustomerCommand request)
        {
            var entity = _mapper.Map<Domain.Customer>(request.CustomerDto);
            await _unitOfWork.CustomerRepository.Add(entity);
            await _unitOfWork.Save();
        }

        private async Task<BaseResponseObj<CreateCustomerDto>> InvalidResponse(BaseResponseObj<CreateCustomerDto> response, ValidationResult validationResult)
        {
            response.Success = false;
            response.Message = validationResult.Errors.Select(q => q.ErrorMessage).First();
            response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();

            return response;
        } 
        #endregion
    }
}
