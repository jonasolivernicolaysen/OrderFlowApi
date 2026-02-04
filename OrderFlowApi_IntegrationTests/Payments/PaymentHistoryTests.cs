using System.Net;
using System.Net.Http.Json;
using Xunit;
using OrderFlowApi_IntegrationTests.Infrastructure;
using OrderFlowApi.Models.DTOs;

namespace OrderFlowApi_IntegrationTests.Payments;

public class PaymentHistoryTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public PaymentHistoryTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task PaymentHistory_ShouldReturnPayments_ForOrder()
    {
        var product = await (await _client.PostAsJsonAsync("/api/products",
            new CreateProductDto
            {
                ProductName = "History product",
                Price = 20,
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

        await _client.PostAsJsonAsync(
            $"/api/orders/{order!.OrderId}/pay",
            new { AccountNumber = 123 });

        var response = await _client.GetAsync(
            $"/api/orders/{order.OrderId}/payments");


        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
