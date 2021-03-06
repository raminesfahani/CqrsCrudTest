
using FluentValidation;
using FluentValidation.Results;
using IbanNet;
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
    public class BankAccountNumberValidator : IBankAccountNumberValidator
    {
        public BankAccountNumberValidator()
        {
            
        }

        public bool Validate(string BankAccountNumber)
        {
            IIbanValidator validator = new IbanValidator();
            var validationResult = validator.Validate(BankAccountNumber);
            if (!validationResult.IsValid)
                return false;

            return true;
        }
    }
}
