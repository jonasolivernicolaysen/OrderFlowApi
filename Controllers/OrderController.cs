using Microsoft.AspNetCore.Mvc;
using OrderFlowApi.Exceptions;
using OrderFlowApi.Mappers;
using OrderFlowApi.Models;
using OrderFlowApi.Models.DTOs;
using OrderFlowApi.Services;
using OrderFlowApi.User;
namespace OrderFlowApi.Controllers
{
    [ApiController]
    [Route("/api/orders")]
    public class OrderController : ControllerBase
    {
        private readonly OrderService _orderService;

        public OrderController(OrderService orderService)
        {
            _orderService = orderService;
        }

        // create order
        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderDto dto)
        {
            var userId = FakeUserLogic.GetCurrentUserId();
            var order = await _orderService.CreateOrderAsync(dto, userId);

            return Ok(OrderMapper.ToDto(order));
        }

        // get order by order id
        [HttpGet("{orderId:guid}")]
        public async Task<IActionResult> GetOrderById(Guid orderId)
        {
            var userId = FakeUserLogic.GetCurrentUserId();
            var order = await _orderService.GetOrderByIdAsync(orderId, userId);

            return Ok(OrderMapper.ToDto(order)); 
        }

        // update order if possible
        [HttpPut("{orderId:guid}")]
        public async Task<IActionResult> UpdateOrder(Guid orderId, UpdateOrderDto dto)
        {
            var userId = FakeUserLogic.GetCurrentUserId();
            var result = await _orderService.UpdateOrderAsync(orderId, dto, userId);

            return Ok(OrderMapper.ToDto(result));
        }

        // cancel order
        [HttpPut("{orderId:guid}/cancel")]
        public async Task<IActionResult> CancelOrder(Guid orderId)
        { 
            var userId = FakeUserLogic.GetCurrentUserId();
            var result = await _orderService.CancelOrderAsync(orderId, userId);
            return Ok(OrderMapper.ToDto(result));
        }

        // pay for order
        [HttpPost("{orderId:guid}/pay")]
        public async Task<IActionResult> PayForOrder(AccountNumberDto dto, Guid orderId)
        {
            var userId = FakeUserLogic.GetCurrentUserId();
            var result = await _orderService.PayForOrderAsync(orderId, dto.AccountNumber, userId);
            return Ok(PaymentMapper.ToDto(result));
        }
    }
}
