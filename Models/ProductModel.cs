using System.ComponentModel.DataAnnotations;

namespace OrderFlowApi.Models
{

    // update productmodel to include createdat and lastupdatedat, maybe add stock quantity later
    public class ProductModel
    {
        [Key]
        public Guid ProductId { get; set; }
        [Required]
        public int CreatedByUserId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdatedAt { get; set; }
    }
}
