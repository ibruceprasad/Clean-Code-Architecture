using FluentAssertions;
using libraries.domain;
using library.Services.Domain.Dtos;
using library___api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;

namespace Library.Api.Integration.Test.IntegrationTests
{
    [Collection("LibraryAPI")]
    public class UpdateBookApiTest  // : IClassFixture<WebApplicationFactory<IApiMarker>>
    {
        private readonly HttpClient _httpClient;
        private const string version = "v1";

        public UpdateBookApiTest(CustomWebApplicationFactory appFactory)
        {
            _httpClient = appFactory.CreateClient();
        }

        [Fact]
        public async Task UpdateBook_ReturnAddedBook_GivenBookDetails()
        {

            // Arrange
            var existingBookDetail = new BookDto(2, "Othello", 300);
            var bookDetailChanges = new BookDto(2, "Othello", 302);
            var id = 2;
        
            var uriWithQuery = $"{version}/book/{id}";

            // Act

            HttpResponseMessage response = await _httpClient.PutAsync(uriWithQuery, JsonContent.Create(bookDetailChanges));

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            var book = await response.Content.ReadFromJsonAsync<BookDto>();
            book.Should().NotBeNull();
            book.Should().BeOfType<BookDto>();
            book.Price.Should().Be(302);

          
        }

        [Fact]
        public async Task UpdateBook_ReturnNotFound_GivenBookDetailsWithInvalidId()
        {
            // Arrange
            var existingBookDetail = new BookDto(2, "Othello", 300);
            var id = 2;
            var uriWithQuery = $"{version}/book/{id}";
            var bookDetailChanges = new BookDto(101, "Othello", 302);

            // Act
            HttpResponseMessage response = await _httpClient.PutAsync(uriWithQuery, JsonContent.Create(bookDetailChanges));

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
            var message = await response.Content.ReadFromJsonAsync<ProblemDetails>();
            message.Should().NotBeNull();
            message.Should().BeOfType<ProblemDetails>();
            message.Detail.Should().NotBeNullOrEmpty();
        }

    }
}