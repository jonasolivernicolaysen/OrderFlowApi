namespace OrderFlowApi.Models
{
    public class PaymentModel
    {
        public int OrderId { get; set; }
        public PaymentStatus Status { get; set; }
        public DateTime PaidAt { get; set; }
    }
}
