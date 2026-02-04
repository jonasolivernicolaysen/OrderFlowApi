using System.ComponentModel.DataAnnotations;

namespace OrderFlowApi.Models.DTOs
{
    public class UpdateProductDto
    {
        public string ProductName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
