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
    public class GetBookApiTest  // : IClassFixture<WebApplicationFactory<IApiMarker>>
    {
        private readonly HttpClient _httpClient;
        private const string version = "v1";
        public GetBookApiTest(CustomWebApplicationFactory appFactory)
        {
            _httpClient = appFactory.CreateClient();
        }

        [Fact]
        public async Task GetBook_ReturnBookDetail_GivenBookId()
        {
            // Act
            HttpResponseMessage response = await _httpClient.GetAsync($"{version}/book/1");

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            var book = await response.Content.ReadFromJsonAsync<BookDto>();
            book.Should().NotBeNull();
            book.Should().BeOfType<BookDto>();
            var expected = new BookDto(1, "Hamlet", 200);
            book.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task GetBook_ReturnNotFound_GivenInvalidBookId()
        {
            // Act
            HttpResponseMessage response = await _httpClient.GetAsync($"{version}/book/-1");

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
            var message = await response.Content.ReadFromJsonAsync<ProblemDetails>();
            message.Should().NotBeNull();
            message.Should().BeOfType<ProblemDetails>();
            message.Detail.Should().NotBeNullOrEmpty();
        }

    }
}