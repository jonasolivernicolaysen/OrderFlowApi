using OrderFlowApi.Models;
using OrderFlowApi.Models.DTOs;

namespace OrderFlowApi.Mappers
{
    public class OrderMapper
    {
        public static OrderModel ToOrderModel(CreateOrderDto dto, int userId)
        {
            return new OrderModel
            {
                UserId = userId,
                ProductId = dto.ProductId,
                Quantity = dto.Quantity,
                Status = OrderStatus.Pending,
                CreatedAt = DateTime.UtcNow,
                LastUpdatedAt = DateTime.UtcNow
            };
        }

        public static OrderDto ToDto(OrderModel order)
        {
            return new OrderDto
            {
                OrderId = order.OrderId,
                ProductId = order.ProductId,
                Quantity = order.Quantity,
                Status = order.Status,
                CreatedAt = order.CreatedAt,
                LastUpdatedAt = order.LastUpdatedAt
            };
        }
    }
}