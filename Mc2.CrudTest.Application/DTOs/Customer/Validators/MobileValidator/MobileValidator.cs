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
    public class MobileValidator : IMobileValidator
    {
        private readonly PhoneNumberUtil _phoneNumberUtil;
        public MobileValidator()
        {
            _phoneNumberUtil = PhoneNumberUtil.GetInstance();
        }

        public bool Validate(string MobileNumber)
        {
            MobileNumber = MobileNumber.Trim()
                                        .Replace(" ", "")
                                        .Replace("-", "");

            if (string.IsNullOrEmpty(MobileNumber))
            {
                return false;
            }

            PhoneNumber numberProto = _phoneNumberUtil.Parse(MobileNumber.ToString(), "");
            bool isValidNumber = _phoneNumberUtil.IsValidNumber(numberProto);
            string region = _phoneNumberUtil.GetRegionCodeForNumber(numberProto);
            bool isValidRegion = _phoneNumberUtil.IsValidNumberForRegion(numberProto, region);
            if(!isValidNumber || !isValidRegion)
                return false;

            return true;
        }
    }
}
