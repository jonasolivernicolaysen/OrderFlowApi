using System.Net;
using System.Net.Http.Json;
using Xunit;
using OrderFlowApi_IntegrationTests.Infrastructure;
using OrderFlowApi.Models.DTOs;

namespace OrderFlowApi_IntegrationTests.Orders;

public class OrderNegativeTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public OrderNegativeTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    // cannot create order with missing product
    [Fact]
    public async Task CreateOrder_ShouldReturn404_WhenProductDoesNotExist()
    {
        var response = await _client.PostAsJsonAsync("/api/orders",
            new CreateOrderDto
            {
                ProductId = Guid.NewGuid(), // non-existent
                Quantity = 1
            });

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    // paying twice should not create duplicate payment
    [Fact]
    public async Task PayForOrder_Twice_ShouldReturnSamePayment()
    {
        // create product
        var product = await (await _client.PostAsJsonAsync("/api/products",
            new CreateProductDto
            {
                ProductName = "Double pay",
                Price = 10,
                Description = ""
            }))
            .Content.ReadFromJsonAsync<ProductDto>();

        // create order
        var order = await (await _client.PostAsJsonAsync("/api/orders",
            new CreateOrderDto
            {
                ProductId = product!.ProductId,
                Quantity = 1
            }))
            .Content.ReadFromJsonAsync<OrderDto>();

        // first payment
        var firstPayment = await (await _client.PostAsJsonAsync(
            $"/api/orders/{order!.OrderId}/pay",
            new { AccountNumber = 123 }))
            .Content.ReadAsStringAsync();

        // second payment
        var secondResponse = await _client.PostAsJsonAsync(
            $"/api/orders/{order.OrderId}/pay",
            new { AccountNumber = 123 });

        var secondPayment = await secondResponse.Content.ReadAsStringAsync();

        Assert.Equal(HttpStatusCode.OK, secondResponse.StatusCode);

        // ensure same payment returned (idempotent)
        Assert.Equal(firstPayment, secondPayment);
    }
}
