using library___api.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace library___api.Middleware
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;
        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            _logger.LogError(exception, "An unhandled exception occurred.");
            ProblemDetails problemDetails;
            httpContext.Response.ContentType = "application/json";

            switch (exception)
            {
                case ApplicationException ex:
                    problemDetails = new ProblemDetails()
                    {
                        Title = "An error occurred",
                        Status = StatusCodes.Status500InternalServerError,
                        Detail = ex?.Message ?? "Application Exception"
                    };
                    break;

                default:
                    problemDetails = new ProblemDetails()
                    {
                        Title = "An error occurred",
                        Status = StatusCodes.Status500InternalServerError,
                        Detail = exception?.Message ?? "Application Exception"
                    };
                    break;
            }
            
            httpContext.Response.StatusCode = problemDetails.Status.Value;

            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
            //return new ValueTask<bool>(true);
        }
    }
}
