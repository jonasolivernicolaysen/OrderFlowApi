namespace OrderFlowApi.Models
{
    public class OrderItemModel
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public int UnitPrice { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
