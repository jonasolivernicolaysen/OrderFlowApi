using System.Net;
using System.Net.Http.Json;
using Xunit;
using OrderFlowApi_IntegrationTests.Infrastructure;
using OrderFlowApi.Models.DTOs;

namespace OrderFlowApi_IntegrationTests.Orders;

public class CreateOrderTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public CreateOrderTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task CreateOrder_ShouldReturnOk_WhenValid()
    {
        // create product first
        var product = await (await _client.PostAsJsonAsync("/api/products",
            new CreateProductDto
            {
                ProductName = "Order product",
                Price = 50,
                Description = ""
            }))
            .Content.ReadFromJsonAsync<ProductDto>();

        var response = await _client.PostAsJsonAsync("/api/orders",
            new CreateOrderDto
            {
                ProductId = product!.ProductId,
                Quantity = 2
            });

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
