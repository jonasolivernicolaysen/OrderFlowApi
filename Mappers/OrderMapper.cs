using OrderFlowApi.Models;
using OrderFlowApi.Models.DTOs;

namespace OrderFlowApi.Mappers
{
    public class OrderMapper
    {
        public static CreateOrderDto ToDto(OrderModel order)
        {
            return new CreateOrderDto
            {
                ProductId = order.ProductId,
                Quantity = order.Quantity
            };
        }

        public static OrderModel ToCreateModel(CreateOrderDto dto, int userId)
        {
            return new OrderModel
            {
                OrderId = Guid.NewGuid(),
                UserId = userId,
                ProductId = dto.ProductId,
                Quantity = dto.Quantity,
                Status = OrderStatus.Received,
                CreatedAt = DateTime.UtcNow,
                LastUpdatedAt = DateTime.UtcNow
            };
        }

        public static CreateOrderDto ToCreateDto(OrderModel dto)
        {
            return new CreateOrderDto
            {
                ProductId = dto.ProductId,
                Quantity = dto.Quantity
            };
        }

        public static UpdateOrderDto ToUpdateDto(OrderModel dto)
        {
            return new UpdateOrderDto
            {
                OrderId = dto.OrderId,
                ProductId = dto.ProductId,
                Quantity = dto.Quantity
            };
        }
    }
}
