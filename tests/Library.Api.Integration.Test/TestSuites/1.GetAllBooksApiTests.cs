using FluentAssertions;
using library.Services.Domain.Dtos;
using library___api;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;

namespace Library.Api.Integration.Test.IntegrationTests
{
    [Collection("LibraryAPI")]
    public class GetAllBooksApiTest  // : IClassFixture<WebApplicationFactory<IApiMarker>>
    {
        private readonly HttpClient _httpClient;
        private const string version = "v1";
        public GetAllBooksApiTest(CustomWebApplicationFactory appFactory)
        {
            _httpClient = appFactory.CreateClient();
        }

        [Fact]
        public async Task GetAll_ReturnAllBooks_WhenNoArguments()
        {
            // Act
            HttpResponseMessage response = await _httpClient.GetAsync($"{version}/books");

            // Assert ( Book id #1 and #2 are inserted by database migrations)
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            var books = await response.Content.ReadFromJsonAsync<List<BookDto>>();
            books.Should().NotBeNull();
            books.Should().BeOfType<List<BookDto>>();
        }



    }
}