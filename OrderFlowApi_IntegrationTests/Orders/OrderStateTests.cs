using System.Net;
using System.Net.Http.Json;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using OrderFlowApi_IntegrationTests.Infrastructure;
using OrderFlowApi.Models;
using OrderFlowApi.Models.DTOs;
using OrderFlowApi.Infrastructure;

namespace OrderFlowApi_IntegrationTests.Orders;

public class OrderStateTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;
    private readonly CustomWebApplicationFactory _factory;

    public OrderStateTests(CustomWebApplicationFactory factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
    }

    // helper: create product + order
    private async Task<(ProductDto product, OrderDto order)> CreatePendingOrder()
    {
        var product = await (await _client.PostAsJsonAsync("/api/products",
            new CreateProductDto
            {
                ProductName = "State test",
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

        return (product!, order!);
    }

    // updating a paid order should fail
    [Fact]
    public async Task UpdateOrder_ShouldFail_WhenOrderIsPaid()
    {
        var (product, order) = await CreatePendingOrder();

        await _client.PostAsJsonAsync(
            $"/api/orders/{order.OrderId}/pay",
            new { AccountNumber = 123 });

        // attempt update
        var response = await _client.PutAsJsonAsync(
            $"/api/orders/{order.OrderId}",
            new UpdateOrderDto
            {
                OrderId = order.OrderId,
                ProductId = product.ProductId,
                Quantity = 5
            });

        Assert.NotEqual(HttpStatusCode.OK, response.StatusCode);
    }

    // updating a SHIPPED order should fail
    [Fact]
    public async Task UpdateOrder_ShouldFail_WhenOrderIsShipped()
    {
        var (product, order) = await CreatePendingOrder();


        using var scope = _factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        var dbOrder = await db.Orders.FindAsync(order.OrderId);
        dbOrder!.Status = OrderStatus.Shipped;
        await db.SaveChangesAsync();

        // attempt update
        var response = await _client.PutAsJsonAsync(
            $"/api/orders/{order.OrderId}",
            new UpdateOrderDto
            {
                OrderId = order.OrderId,
                ProductId = product.ProductId,
                Quantity = 5
            });

        Assert.NotEqual(HttpStatusCode.OK, response.StatusCode);
    }

    // updating a cancelled order should fail
    [Fact]
    public async Task UpdateOrder_ShouldFail_WhenOrderIsCancelled()
    {
        var (product, order) = await CreatePendingOrder();

        // cancel via API
        await _client.PutAsync($"/api/orders/{order.OrderId}/cancel", null);

        // attempt update
        var response = await _client.PutAsJsonAsync(
            $"/api/orders/{order.OrderId}",
            new UpdateOrderDto
            {
                OrderId = order.OrderId,
                ProductId = product.ProductId,
                Quantity = 5
            });

        Assert.NotEqual(HttpStatusCode.OK, response.StatusCode);
    }
}
