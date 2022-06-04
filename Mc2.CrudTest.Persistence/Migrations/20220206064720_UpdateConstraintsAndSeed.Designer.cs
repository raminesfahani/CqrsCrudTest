﻿// <auto-generated />
using System;
using Mc2.CrudTest.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Mc2.CrudTest.Persistence.Migrations
{
    [DbContext(typeof(CustomerManagementDbContext))]
    [Migration("20220206064720_UpdateConstraintsAndSeed")]
    partial class UpdateConstraintsAndSeed
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.8")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Mc2.CrudTest.Domain.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("BankAccountNumber")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Firstname")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Lastname")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.ToTable("Customers");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            BankAccountNumber = 7617238,
                            DateCreated = new DateTime(2022, 2, 6, 10, 17, 19, 972, DateTimeKind.Local).AddTicks(8000),
                            DateOfBirth = new DateTime(1990, 11, 21, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "r.esfahani@yahoo.com",
                            Firstname = "Ramin",
                            LastModifiedDate = new DateTime(2022, 2, 6, 10, 17, 19, 977, DateTimeKind.Local).AddTicks(5346),
                            Lastname = "Esfahani",
                            PhoneNumber = "+989120345399"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
