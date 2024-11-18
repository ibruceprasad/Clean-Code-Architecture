using Library.Repository.Repositories;
using library.services.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using library.repository;
using Library.Services.Contracts;

namespace Library.Repository
{
    public static class DatabaseServiceRegistration
    {
        public static IServiceCollection AddDatabaseServices(this IServiceCollection services,
               IConfiguration configuration)
        {
            services.AddDbContext<LibraryDbContext>();
            services.AddScoped(typeof(IDbRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IBookRepository, BookRepository>();
            return services;
        }
    }
}
