using OrderFlowApi.Models;
using OrderFlowApi.Models.DTOs;

namespace OrderFlowApi.Mappers
{
    public class PaymentMapper
    {
        public static PaymentModel ToPaymentModel(Guid orderId, int payingAccount, int receivingAccount, PaymentStatus status)
        {
            return new PaymentModel
            {
                OrderId = orderId,
                PayingAccount = payingAccount,
                ReceivingAccount = receivingAccount,
                Status = status,
                PaidAt = DateTime.UtcNow
            };
        }

        public static PaymentDto ToDto(PaymentModel product)
        {
            return new PaymentDto
            {
                AccountNumber = product.PayingAccount,
                Status = product.Status
            };
        }
    }
}
