using FluentAssertions;
using library.Services.Domain.Dtos;
using library___api;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;

namespace Library.Api.Integration.Test.IntegrationTests
{
    [Collection("LibraryAPI")]
    public class CommonTest  // : IClassFixture<WebApplicationFactory<IApiMarker>>
    {
        private readonly HttpClient _httpClient;
        public CommonTest(CustomWebApplicationFactory appFactory)
        {
            _httpClient = appFactory.CreateClient();
        }
        [Fact]
        public async Task ReturnErrorResponse_GivenInvalidApi()
        {
            // Act
            HttpResponseMessage response = await _httpClient.GetAsync(Constants.invalidUrl);
            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);

        }

    }
}