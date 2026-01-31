using System.ComponentModel.DataAnnotations;

namespace OrderFlowApi.Models
{
    public class PaymentModel
    {
        [Required]
        public Guid OrderId { get; set; }

        public PaymentStatus Status { get; set; }

        public DateTime PaidAt { get; set; }
    }
}
