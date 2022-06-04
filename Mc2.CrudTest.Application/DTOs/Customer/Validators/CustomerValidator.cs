using AutoMapper;
using FluentValidation;
using Mc2.CrudTest.Application.DTOs.Customer;
using Mc2.CrudTest.Application.DTOs.Customer.Validators.Common.EmailValidator;
using Mc2.CrudTest.Application.Persistence;
using PhoneNumbers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Application.DTOs.Customer.Validators
{
    public class CustomerValidator : AbstractValidator<CustomerDto>
    {
        public CustomerValidator(
            IBankAccountNumberValidator bankAccountNumberValidator,
            IMobileValidator mobileValidator,
            IDuplicateCustomerValidator duplicateCustomerValidator,
            IEmailValidator emailValidator,
            IMapper mapper)
        {

            RuleFor(x => x.BankAccountNumber)
                .Must(x => bankAccountNumberValidator.Validate(x))
                .WithMessage("{PropertyName} is invalid!");

            RuleFor(x => x.Email)
                .Must(x => emailValidator.Validate(x))
                .WithMessage("{PropertyName} is invalid!");

            RuleFor(x => x.PhoneNumber)
                .Must(x => mobileValidator.Validate(x))
                .WithMessage("{PropertyName} is invalid!");

            When(x => x.Id == 0, () => {
                RuleFor(x => x)
                    .MustAsync((x, token) => duplicateCustomerValidator.ValidateUniqueCustomer(x))
                    .WithMessage("Duplicate customer is found!");
            })
            .Otherwise(() => {
                RuleFor(x => x)
                        .MustAsync((x, token) => {
                            var customer = mapper.Map<UpdateCustomerDto>(x);
                            return duplicateCustomerValidator.ValidateDuplicateCustomer(customer);
                        })
                        .WithMessage("Duplicate customer is found!");
            });

        }
    }
}
