using OrderFlowApi.Models;
using OrderFlowApi.Models.DTOs;

namespace OrderFlowApi.Mappers
{
    public class ProductMapper
    {
        public static ProductModel ToModel(ProductDto product)
        {
            return new ProductModel
            {
                ProductName = product.ProductName,
                Price = product.Price,
                Description = product.Description
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
