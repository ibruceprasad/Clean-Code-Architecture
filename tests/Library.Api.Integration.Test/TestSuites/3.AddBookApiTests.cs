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
    public class AddBookApiTest  // : IClassFixture<WebApplicationFactory<IApiMarker>>
    {
        private readonly HttpClient _httpClient;
        private const string version = "v1";
        public AddBookApiTest(CustomWebApplicationFactory appFactory)
        {
            _httpClient = appFactory.CreateClient();
        }

        [Fact]
        public async Task AddBook_ReturnAddedBook_GivenBookDetails()
        {

            // Arrange
            var newBook = new BookDto(-1, "Capital", 300);

            // Act
            HttpResponseMessage response = await _httpClient.PostAsync($"{version}/book", JsonContent.Create(newBook));

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            var book = await response.Content.ReadFromJsonAsync<BookDto>();
            book.Should().NotBeNull();
            book.Should().BeOfType<BookDto>();
            book.Name.Should().Be("Capital");
            book.Price.Should().Be(300);
        }

        [Fact]
        public async Task AddBook_ReturnBadRequest_GivenBookDetailsWithInvalidPrice()
        {
            // Arrange
            var newBook = new BookDto(0, "Capital", 300000);

            // Act
            HttpResponseMessage response = await _httpClient.PostAsync($"{version}/book", JsonContent.Create(newBook));

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
            var message = await response.Content.ReadFromJsonAsync<ProblemDetails>();
            message.Should().NotBeNull();
            message.Should().BeOfType<ProblemDetails>();
            message.Detail.Should().NotBeNullOrEmpty();
        }

    }
}