using OrderFlowApi.Models;
using OrderFlowApi.Models.DTOs;

namespace OrderFlowApi.Mappers
{
    public class ProductMapper
    {
        public static ProductModel ToProductModel(CreateProductDto product, int userId)
        {
            return new ProductModel
            {
                ProductName = product.ProductName,
                CreatedByUserId = userId,
                Price = product.Price,
                Description = product.Description,
                CreatedAt = DateTime.UtcNow,
                LastUpdatedAt = DateTime.UtcNow
            };
        }

        public static ProductDto ToDto(ProductModel product)
        {
            return new ProductDto
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                Price = product.Price,
                Description = product.Description
            };
        }
    }
}
