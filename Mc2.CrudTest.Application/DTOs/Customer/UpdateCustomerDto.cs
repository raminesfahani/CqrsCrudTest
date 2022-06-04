using Mc2.CrudTest.Application.DTOs.Common;
using Mc2.CrudTest.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mc2.CrudTest.Application.DTOs.Customer
{
    public class UpdateCustomerDto : BaseDto, ICustomerDto
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }
        public string BankAccountNumber { get; set; }
    }
}
