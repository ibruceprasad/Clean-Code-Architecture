using library___api; 
using library___api.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Configure services
builder.ConfigureServices();

var app = builder.Build();

// Exception handler
app.UseMiddleware<ExceptionHandlerMiddleware>();


// Configure pipeline
app.ConfigurePipeline();

// Configire api endpoints
app.ConfigureBooksEndPoints();

app.Run();