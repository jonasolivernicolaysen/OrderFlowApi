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
            try
            {
            var userId = FakeUserLogic.GetCurrentUserId();
            var order = await _orderService.CreateOrderAsync(dto, userId);
            if (order == null)
                return BadRequest("Invalid request");

            return Ok(OrderMapper.ToDto(order));
            }
            catch (BadOrderException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // get order by order id
        [HttpGet("{orderId:guid}")]
        public async Task<IActionResult> GetOrderById(Guid orderId)
        {
            try
            {
            var result = await _orderService.GetOrderByIdAsync(orderId);
            if (result == null)
                return BadRequest("Order not found");

            return Ok(result);
            }
            catch (OrderNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (UserNotAuthorizedException ex)
            {
                return Forbid(ex.Message);
            }
        }

        // update order if possible
        [HttpPut("{orderId}")]
        public async Task<IActionResult> UpdateOrder(Guid orderId, UpdateOrderDto dto)
        {
            try
            {
            var userId = FakeUserLogic.GetCurrentUserId();
            var result = await _orderService.UpdateOrderAsync(orderId, dto, userId);
            if (result == null)
                return BadRequest("Invalid request");

            return Ok(OrderMapper.ToDto(result));
            }
            catch (OrderNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (UserNotAuthorizedException ex)
            {
                return Forbid(ex.Message);
            }
            catch (InvalidOrderStateException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // cancel order
    }
}
