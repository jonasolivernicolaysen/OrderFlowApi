using System.Reflection.Metadata.Ecma335;


namespace OrderFlowApi.Models.DTOs
{
    public class UpdateOrderDto
    {
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
