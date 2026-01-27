using System.Reflection.Metadata.Ecma335;


namespace OrderFlowApi.Models.DTOs
{
    public class OrderDto
    {
        public int OrderId { get; set; }
        public OrderStatus Status { get; set; }
        public int TotalAmount { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
