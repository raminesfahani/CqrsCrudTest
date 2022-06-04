using FluentValidation;
using Mc2.CrudTest.Application.DTOs.Customer;
using Mc2.CrudTest.Application.DTOs.Customer.Validators;
using Mc2.CrudTest.Application.DTOs.Customer.Validators.Common.EmailValidator;
using Mc2.CrudTest.Domain;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using PhoneNumbers;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Mc2.CrudTest.Application
{
    public static class ApplicationServicesRegistration
    {
        public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddScoped<IMobileValidator, MobileValidator>();
            services.AddScoped<IEmailValidator, EmailValidator>();
            services.AddScoped<IBankAccountNumberValidator, BankAccountNumberValidator>();
            services.AddScoped<IDuplicateCustomerValidator, DuplicateCustomerValidator>();


            services.AddTransient<CustomerValidator>();

            return services;
        }
    }
}
