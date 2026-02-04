using System.Net;
using System.Net.Http.Json;
using Xunit;
using OrderFlowApi_IntegrationTests.Infrastructure;
using OrderFlowApi.Models.DTOs;

namespace OrderFlowApi_IntegrationTests.Orders;

public class PayForOrderTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public PayForOrderTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task PayForOrder_ShouldSucceed_WhenPending()
    {
        var product = await (await _client.PostAsJsonAsync("/api/products",
            new CreateProductDto
            {
                ProductName = "Pay product",
                Price = 10,
                Description = ""
            }))
            .Content.ReadFromJsonAsync<ProductDto>();

        var order = await (await _client.PostAsJsonAsync("/api/orders",
    new CreateOrderDto
    {
        ProductId = product!.ProductId,
        Quantity = 1
    }))
    .Content.ReadFromJsonAsync<OrderDto>();

        Assert.NotNull(order);

        var payResponse = await _client.PostAsJsonAsync(
            $"/api/orders/{order!.OrderId}/pay",
            new { AccountNumber = 123 });


        Assert.Equal(HttpStatusCode.OK, payResponse.StatusCode);
    }
}
