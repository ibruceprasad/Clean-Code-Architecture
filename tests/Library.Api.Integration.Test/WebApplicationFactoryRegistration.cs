using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using library.repository;
using library___api;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Api.Integration.Test
{
    public class CustomWebApplicationFactory : WebApplicationFactory<IApiMarker>, IAsyncLifetime
    {
        private IContainer dbContainer= default!;
        string connectionString = string.Empty;
        private const string Database = "librarydb-integration";
        private const string Username = "sa";
        private const string Password = "$trongPassword";
        private const ushort MsSqlPort = 1433; 
        private const ushort HostPort = 1533;
        public async Task InitializeAsync()
        {
            // Create SQLServer database container for integration testing
            dbContainer = new ContainerBuilder()
            .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
            .WithPortBinding(HostPort,MsSqlPort)
            .WithEnvironment("ACCEPT_EULA", "Y")
            .WithEnvironment("SQLCMDUSER", Username)
            .WithEnvironment("SQLCMDPASSWORD", Password)
            .WithEnvironment("MSSQL_SA_PASSWORD", Password)
            .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(MsSqlPort))
            .Build();
            await dbContainer.StartAsync();
            var host = dbContainer.Hostname;
            var port = dbContainer.GetMappedPublicPort(MsSqlPort);
            // Replace connection string in DbContext
            connectionString = $"Server={host},{port};Database={Database};User Id={Username};Password={Password};TrustServerCertificate=True";
        }
        public new async Task DisposeAsync()
        {
            await dbContainer.DisposeAsync();
        }
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        { 
    
            builder.ConfigureServices(services =>
            {
                services.Remove(services.SingleOrDefault(service => typeof(DbContextOptions<LibraryDbContext>) == service.ServiceType));
                services.Remove(services.SingleOrDefault(service => typeof(DbConnection) == service.ServiceType));
                services.AddDbContext<LibraryDbContext>((_, option) => option.UseSqlServer(connectionString));
                var serviceProvider = services.BuildServiceProvider();
                using (var context = serviceProvider.GetRequiredService<LibraryDbContext>())
                {
                    // Now you can use your context here
                    var isSqlServer = context.Database.IsSqlServer();
                    var conn = context.Database.GetDbConnection();
                    context.Database.Migrate();
                }
            });
            base.ConfigureWebHost(builder);
        }
    }

    [CollectionDefinition("LibraryAPI")]
    public class CustomWebAppCollection : ICollectionFixture<CustomWebApplicationFactory>
    {
        // No code needed here; it's only used for collection registration
    }
}
