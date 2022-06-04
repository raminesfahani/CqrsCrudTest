using Mc2.CrudTest.Application.Persistence;
using Mc2.CrudTest.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mc2.CrudTest.Persistence
{
    public static class PersistenceServicesRegistration
    {
        public static IServiceCollection ConfigurePersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CustomerManagementDbContext>(options =>
               options.UseSqlServer(
                   configuration.GetConnectionString("CustomerManagementConnectionString")));


            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<ICustomerRepository, CustomerRepository>();

            return services;
        }
    }
}
