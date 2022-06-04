using FluentValidation;
using FluentValidation.Results;
using Mc2.CrudTest.Application.DTOs.Customer;
using Mc2.CrudTest.Application.Persistence;
using PhoneNumbers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Application.DTOs.Customer.Validators
{
    public class DuplicateCustomerValidator : IDuplicateCustomerValidator
    {
        private readonly ICustomerRepository _customerRepository;
        public DuplicateCustomerValidator(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<bool> ValidateDuplicateCustomer(UpdateCustomerDto Customer)
        {
            var customer = await _customerRepository.Get(Customer.Id);

            var canUpdateEmail = await _customerRepository.CanUpdateNewEmail(customer, Customer.Email);
            if (!canUpdateEmail)
                return false;

            var CanUpdateNewInformation = await _customerRepository.CanUpdateNewInformation(customer, Customer.Firstname, Customer.Lastname, Customer.DateOfBirth);
            if (!CanUpdateNewInformation)
                return false;

            return true;
        }

        public async Task<bool> ValidateUniqueCustomer(CustomerDto Customer)
        {
            if((await _customerRepository.IsInformationUnique(Customer.Firstname, Customer.Lastname, Customer.DateOfBirth)) == false)
                return false;

            if ((await _customerRepository.IsEmailUnique(Customer.Email)) == false)
                return false;

            return true;
        }
    }
}
