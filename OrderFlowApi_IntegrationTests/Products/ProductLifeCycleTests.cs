using System.Net;
using System.Net.Http.Json;
using Xunit;
using OrderFlowApi_IntegrationTests.Infrastructure;
using OrderFlowApi.Models.DTOs;

namespace OrderFlowApi_IntegrationTests.Products;

public class ProductLifeCycleTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public ProductLifeCycleTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Product_CanBeCreated_Updated_AndDeleted()
    {
        // CREATE
        var createResponse = await _client.PostAsJsonAsync("/api/products",
            new CreateProductDto
            {
                ProductName = "Test product",
                Price = 100,
                Description = "desc"
            });

        Assert.Equal(HttpStatusCode.OK, createResponse.StatusCode);

        var product = await createResponse.Content.ReadFromJsonAsync<ProductDto>();
        Assert.NotNull(product);

        // UPDATE
        var updateResponse = await _client.PutAsJsonAsync(
            $"/api/products/{product!.ProductId}",
            new UpdateProductDto
            {
                ProductName = "Updated",
                Price = 200,
                Description = "updated"
            });

        Assert.Equal(HttpStatusCode.OK, updateResponse.StatusCode);

        // DELETE
        var deleteResponse = await _client.DeleteAsync(
            $"/api/products/{product.ProductId}");

        Assert.Equal(HttpStatusCode.OK, deleteResponse.StatusCode);
    }
}
