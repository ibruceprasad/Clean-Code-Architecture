
using Library.Repository;
using Library.Services;
using Asp.Versioning;

namespace library___api.Extensions
{
    public static class ServiceConfigurationExtensions
    {
        public static IServiceCollection ConfigureServices(this WebApplicationBuilder builder)
        {
            // Add services to the container.
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Add/Register custom services 
            builder.Services.AddCors();
            builder.Services.AddDatabaseServices(builder.Configuration);
            builder.Services.AddApplicationServices(builder.Configuration);
            builder.Services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ApiVersionReader = new UrlSegmentApiVersionReader();
            });
            return builder.Services;
        }


        public static WebApplication ConfigurePipeline(this WebApplication app)
        {
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors(o => o.WithOrigins("http://localhost:3000").AllowAnyHeader().AllowAnyMethod());
            app.UseHttpsRedirection();

            return app;
        }


    }
}
