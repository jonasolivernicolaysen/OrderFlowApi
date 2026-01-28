using System.Reflection.Metadata.Ecma335;


namespace OrderFlowApi.Models.DTOs
{
    public class CreateOrderDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
