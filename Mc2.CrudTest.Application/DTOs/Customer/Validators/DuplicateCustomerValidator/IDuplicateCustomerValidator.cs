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
    public interface IDuplicateCustomerValidator
    {
        public Task<bool> ValidateDuplicateCustomer(UpdateCustomerDto Customer);
        public Task<bool> ValidateUniqueCustomer(CustomerDto Customer);
    }
}
