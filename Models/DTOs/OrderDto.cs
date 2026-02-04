using System.Text.Json.Serialization;

namespace OrderFlowApi.Models.DTOs
{
    public class OrderDto
    {
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public OrderStatus Status { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdatedAt { get; set; }
    }
}
