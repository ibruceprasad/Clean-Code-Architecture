using Microsoft.AspNetCore.Mvc;
using library.Services.Domain.Dtos;
using library.services.Services;
using System.Net;
using Asp.Versioning.Conventions;

namespace library___api.Extensions
{
    public static class EndPointMapExtensions
    {


        public static WebApplication ConfigureBooksEndPoints(this WebApplication app)
        {
            var versionSet = app.NewApiVersionSet()
                .HasApiVersion(1)
                .Build();


            app.MapGet("v{version:apiVersion}/books", async (IBookServices bookServices) =>
            {
                var booksDto = await bookServices.GetAllBooksAsync();
                return Results.Ok(booksDto);
                
            })
            .WithApiVersionSet(versionSet)
            .MapToApiVersion(1)
            .Produces(StatusCodes.Status200OK, typeof(List<BookDto>))
            .Produces(StatusCodes.Status500InternalServerError, typeof(ProblemDetails));
       

            app.MapGet("v{version:apiVersion}/book/{id}", async (int id, IBookServices bookServices) =>
            {
                var bookDto = await bookServices.GetBookByIdAsync(id);

                return bookDto == null ?
                    Results.NotFound(new ProblemDetails() { Detail = $"Book id: {id} not found ", Status = StatusCodes.Status404NotFound }) :
                    Results.Ok(bookDto);
            })
            .WithApiVersionSet(versionSet)
            .MapToApiVersion(1)
            .Produces(StatusCodes.Status200OK, typeof(BookDto))
            .Produces(StatusCodes.Status404NotFound, typeof(ProblemDetails))
            .Produces(StatusCodes.Status500InternalServerError, typeof(ProblemDetails));
            


            app.MapPost("v{version:apiVersion}/book", async ([FromBody] BookDto bookDto,
                                                IBookServices bookServices) =>
            {
                var result = await bookServices.AddBookAsync(bookDto);
                if (result.IsSuccess)
                    return Results.Ok(result.Data);
                return Results.BadRequest(new ProblemDetails() { Detail = result.ErrorMessage, Status = StatusCodes.Status400BadRequest }); ;
            })
            .WithApiVersionSet(versionSet)
            .MapToApiVersion(1)
            .Produces(StatusCodes.Status200OK, typeof(BookDto))
            .Produces(StatusCodes.Status400BadRequest, typeof(ProblemDetails))
            .Produces(StatusCodes.Status500InternalServerError, typeof(ProblemDetails));



            app.MapPut("v{version:apiVersion}/book/{id}", async ( int id,
                                                   [FromBody] BookDto bookDto,
                                                   IBookServices bookServices) =>
            {
                var result = await bookServices.UpdateBookAsync(id, bookDto);

                if (result.IsSuccess)
                    return Results.Ok(result.Data);

                switch (result.Status)
                {
                    case HttpStatusCode.BadRequest:
                        return Results.BadRequest(new ProblemDetails() { Detail = result.ErrorMessage, Status = StatusCodes.Status400BadRequest });
                    case HttpStatusCode.NotFound:
                        return Results.NotFound(new ProblemDetails() { Detail = result.ErrorMessage, Status = StatusCodes.Status404NotFound });
                    default:
                        return Results.Problem(new ProblemDetails() { Status = StatusCodes.Status500InternalServerError, Detail = "Unknown Internal Server Error" });
                }
            })
            .WithApiVersionSet(versionSet)
            .MapToApiVersion(1)
            .Produces(StatusCodes.Status200OK, typeof(BookDto))
            .Produces(StatusCodes.Status400BadRequest, typeof(ProblemDetails))
            .Produces(StatusCodes.Status404NotFound, typeof(ProblemDetails))
            .Produces(StatusCodes.Status500InternalServerError, typeof(ProblemDetails));



            app.MapDelete("v{version:apiVersion}/book/{id}", async (int id,
                                               IBookServices bookServices) =>
            {
                var result = await bookServices.DeleteBookAsync(id);
                if (result.IsSuccess)
                    return Results.Ok();
                return Results.BadRequest(new ProblemDetails() { Detail = result.ErrorMessage , Status = StatusCodes.Status400BadRequest});
            })
            .WithApiVersionSet(versionSet)
            .MapToApiVersion(1)
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound, typeof(ProblemDetails))
            .Produces(StatusCodes.Status500InternalServerError, typeof(ProblemDetails));


            return app;
        }
    }
}
