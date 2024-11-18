using Microsoft.AspNetCore.Mvc;
using library.Services.Domain.Dtos;
using library.services.Services;
using library___api.Models;
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
            .Produces(StatusCodes.Status500InternalServerError, typeof(ErrorResponseMessage));
            

            app.MapGet("v{version:apiVersion}/book/{id}", async (int id, IBookServices bookServices) =>
            {
                var bookDto = await bookServices.GetBookByIdAsync(id);

                return bookDto == null ?
                    Results.NotFound(new ErrorResponseMessage() { ErrorMessage = $"Book id: {id} not found " }) :
                    Results.Ok(bookDto);
            })
            .WithApiVersionSet(versionSet)
            .MapToApiVersion(1)
            .Produces(StatusCodes.Status200OK, typeof(BookDto))
            .Produces(StatusCodes.Status404NotFound, typeof(ErrorResponseMessage))
            .Produces(StatusCodes.Status500InternalServerError, typeof(ErrorResponseMessage));
            


            app.MapPost("v{version:apiVersion}/book", async ([FromBody] BookDto bookDto,
                                                IBookServices bookServices) =>
            {
                var result = await bookServices.AddBookAsync(bookDto);
                if (result.IsSuccess)
                    return Results.Ok(result.Data);
                return Results.BadRequest(new ErrorResponseMessage() { ErrorMessage = result.ErrorMessage }); ;
            })
            .WithApiVersionSet(versionSet)
            .MapToApiVersion(1)
            .Produces(StatusCodes.Status200OK, typeof(BookDto))
            .Produces(StatusCodes.Status400BadRequest, typeof(ErrorResponseMessage))
            .Produces(StatusCodes.Status500InternalServerError, typeof(ErrorResponseMessage));



            app.MapPut("v{version:apiVersion}/book", async ([FromQuery] int id,
                                                   [FromBody] BookDto bookDto,
                                                   IBookServices bookServices) =>
            {
                var result = await bookServices.UpdateBookAsync(id, bookDto);

                if (result.IsSuccess)
                    return Results.Ok(result.Data);

                switch (result.Status)
                {
                    case HttpStatusCode.BadRequest:
                        return Results.BadRequest(new ErrorResponseMessage() { ErrorMessage = result.ErrorMessage });
                    case HttpStatusCode.NotFound:
                        return Results.NotFound(new ErrorResponseMessage() { ErrorMessage = result.ErrorMessage });
                    default:
                        return Results.Problem(statusCode: 500, title: "Unknown Internal Server Error");
                }
            })
            .WithApiVersionSet(versionSet)
            .MapToApiVersion(1)
            .Produces(StatusCodes.Status200OK, typeof(BookDto))
            .Produces(StatusCodes.Status400BadRequest, typeof(ErrorResponseMessage))
            .Produces(StatusCodes.Status404NotFound, typeof(ErrorResponseMessage))
            .Produces(StatusCodes.Status500InternalServerError, typeof(ErrorResponseMessage));



            app.MapDelete("v{version:apiVersion}/book/{id}", async (int id,
                                               IBookServices bookServices) =>
            {
                var result = await bookServices.DeleteBookAsync(id);
                if (result.IsSuccess)
                    return Results.Ok();
                return Results.BadRequest(new ErrorResponseMessage());
            })
            .WithApiVersionSet(versionSet)
            .MapToApiVersion(1)
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound, typeof(ErrorResponseMessage))
            .Produces(StatusCodes.Status500InternalServerError, typeof(ErrorResponseMessage));


            return app;
        }
    }
}
