using Microsoft.AspNetCore.Mvc;
using OrderFlowApi.Models;
using OrderFlowApi.Models.DTOs;
namespace OrderFlowApi.Controllers
{
    [ApiController]
    [Route("/api/orders")]
    public class OrderController : ControllerBase
    {

        // create order
        [HttpPost]
        public async Task<IActionResult> CreateOrder(OrderDto dto)
        {
            return Ok();
        }

        // get order by order id

        // update order if possible

        // cancel order
    }
}
