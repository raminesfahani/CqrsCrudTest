using Mc2.CrudTest.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mc2.CrudTest.Persistence.Entities
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(builder => builder.Id);
            builder.Property(x=>x.Id).ValueGeneratedOnAdd();

            //Data Seeding for inserting some examples records
            //-------------------------------------------------

            builder.HasData(new Customer()
            {
                Id = 1,
                Firstname = "Ramin",
                Lastname = "Esfahani",
                Email = "r.esfahani@yahoo.com",
                BankAccountNumber = "7617238",
                PhoneNumber = 989120345399,
                DateCreated = DateTime.Now,
                DateOfBirth = new DateTime(1990, 11, 21),
                LastModifiedDate = DateTime.Now,
            });
        }
    }
}
