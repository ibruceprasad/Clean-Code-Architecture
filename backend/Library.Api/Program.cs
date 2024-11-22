using library___api; 
using library___api.Extensions;
using library___api.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
// Configure services
builder.ConfigureServices();

var app = builder.Build();

// Exception handler
//app.UseMiddleware<ExceptionHandlerMiddleware>();
app.UseExceptionHandler("/error");


// Configure pipeline
app.ConfigurePipeline();

// Configire api endpoints
app.ConfigureBooksEndPoints();

app.Run();