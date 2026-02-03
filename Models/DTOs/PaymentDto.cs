using System.ComponentModel.DataAnnotations;

namespace OrderFlowApi.Models.DTOs
{
    public class PaymentDto
    {
        [Required]
        public int AccountNumber { get; set; }
        public PaymentStatus Status { get; set; }

    }
}
