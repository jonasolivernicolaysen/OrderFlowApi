using Microsoft.AspNetCore.Mvc;
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
            var result = await _orderService.CreateOrderAsync(dto, userId);
            if (result == null)
                return BadRequest("Invalid request");

            return Ok(OrderMapper.ToCreateDto(result));
        }

        // get order by order id
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetOrderById(Guid orderId)
        {
            var result = await _orderService.GetOrderByIdAsync(orderId);
            if (result == null)
                return BadRequest("Order not found");
            return Ok(result);
        }

        // update order if possible
        [HttpPut("{orderId}")]
        public async Task<IActionResult> UpdateOrder(Guid orderId, UpdateOrderDto dto)
        {
            var userId = FakeUserLogic.GetCurrentUserId();
            var result = await _orderService.UpdateOrderAsync(orderId, dto, userId);
            if (result == null)
                return BadRequest("Invalid request");
            return Ok(OrderMapper.ToCreateDto(result));
        }

        // cancel order
    }
}
