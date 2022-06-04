using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Mc2.CrudTest.Domain
{
    public class Customer : BaseDomainEntity
    {
        [Required]
        [MaxLength(20)]
        public string Firstname { get; set; }
        [Required]
        [MaxLength(20)]
        public string Lastname
        { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public ulong PhoneNumber { get; set; }

        [EmailAddress]
        [Required]
        [MaxLength(100)]
        public string Email
        { get; set; }

        [MaxLength(50)]
        public string BankAccountNumber { get; set; }
    }
}
