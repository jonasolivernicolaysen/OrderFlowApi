using System.Net;
using Xunit;
using OrderFlowApi_IntegrationTests.Infrastructure;

namespace OrderFlowApi_IntegrationTests.Smoke
{
    public class ApiSmokeTests
        : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public ApiSmokeTests(CustomWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Api_Should_Start_And_Respond()
        {
            var response = await _client.GetAsync("/api/products");

            Assert.NotEqual(HttpStatusCode.InternalServerError, response.StatusCode);
        }
    }
}
