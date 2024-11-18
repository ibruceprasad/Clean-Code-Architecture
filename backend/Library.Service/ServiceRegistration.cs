using library.services.Contracts;
using library.services.Services;
using Library.Services.Contracts;
using Library.Services.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services
{
    public  static class ServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services,
              IConfiguration configuration)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<IBookServices, BookServices>();
            services.AddScoped<IDataValidaion, DataValidaion>();
            return services;
        }
    }
}
