using System.Net;
using System.Net.Http.Json;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using OrderFlowApi_IntegrationTests.Infrastructure;
using OrderFlowApi.Models.DTOs;
using OrderFlowApi.Infrastructure;

namespace OrderFlowApi_IntegrationTests.Payments;

public class PaymentAuthorizationTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;
    private readonly CustomWebApplicationFactory _factory;

    public PaymentAuthorizationTests(CustomWebApplicationFactory factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetPayments_ShouldFail_ForDifferentUser()
    {
        // create product
        var product = await (await _client.PostAsJsonAsync("/api/products",
            new CreateProductDto
            {
                ProductName = "Auth test",
                Price = 30,
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

        // Simulate different user by editing DB directly
        using var scope = _factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        var dbOrder = await db.Orders.FindAsync(order!.OrderId);
        dbOrder!.UserId = 999; // different user
        await db.SaveChangesAsync();

        // attempt to access payments
        var response = await _client.GetAsync(
            $"/api/orders/{order.OrderId}/payments");

        Assert.NotEqual(HttpStatusCode.OK, response.StatusCode);
    }
}
