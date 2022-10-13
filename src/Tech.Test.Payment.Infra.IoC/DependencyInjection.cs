using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tech.Test.Payment.Application.Mappings;
using Tech.Test.Payment.Application.Services;
using Tech.Test.Payment.Application.Services.Interfaces;
using Tech.Test.Payment.Domain.Repositories;
using Tech.Test.Payment.Infra.Data.Context;
using Tech.Test.Payment.Infra.Data.Repositories;

namespace Tech.Test.Payment.Infra.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddDbContext<DataContext>(options => options.UseInMemoryDatabase("Database"));
            service.AddScoped<DataContext, DataContext>();

            service.AddScoped<ISaleRepository, SaleRepository>();
            return service;
        }

        public static IServiceCollection AddServices(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddAutoMapper(typeof(DomainToDTOMap));
            service.AddScoped<ISaleService, SaleService>();
            return service;
        }
    }
}