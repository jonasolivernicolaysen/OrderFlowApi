namespace OrderFlowApi.Models.DTOs
{
    public class OrderDto
    {
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public OrderStatus Status { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
