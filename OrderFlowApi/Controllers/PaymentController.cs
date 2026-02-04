using Microsoft.AspNetCore.Mvc;
using OrderFlowApi.Mappers;
using OrderFlowApi.Services;
using OrderFlowApi.User;

namespace OrderFlowApi.Controllers
{
    [ApiController]
    [Route("api/orders/{orderId:guid}/payments")]
    public class PaymentController : ControllerBase
    {
        private readonly PaymentService _paymentService;

        public PaymentController(PaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPayments(Guid orderId)
        {
            var userId = FakeUserLogic.GetCurrentUserId();
            var payments = await _paymentService.GetPaymentsForOrderAsync(orderId, userId);

            return Ok(payments.Select(PaymentMapper.ToDto));
        }
    }
}
