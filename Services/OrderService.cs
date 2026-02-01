using Microsoft.AspNetCore.Http.HttpResults;
using OrderFlowApi.Models.DTOs;
using OrderFlowApi.Models;
using OrderFlowApi.Mappers;
using OrderFlowApi.User;
using OrderFlowApi.Exceptions;


namespace OrderFlowApi.Services
{
    public class OrderService
    {
        private readonly AppDbContext _context;
        public OrderService(AppDbContext context)
        {
            _context = context;
        }

        // create order
        public async Task<OrderModel> CreateOrderAsync(CreateOrderDto dto, int userId)
        {
            var order = OrderMapper.ToOrderModel(dto, userId);
            if (order == null)
                throw new BadOrderException("Order if insufficcient");

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return order;
        }

        // get order by order id
        public async Task<OrderModel?> GetOrderByIdAsync(Guid orderId, int userId)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order == null)
                throw new OrderNotFoundException(orderId);

            if (order.UserId != userId)
                throw new UserNotAuthorizedException();

            return order;
        }

        // update order if possible
        public async Task<OrderModel> UpdateOrderAsync(Guid orderId, UpdateOrderDto dto, int userId)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order == null)
                throw new OrderNotFoundException(orderId);

            if (order.UserId != userId)
                throw new UserNotAuthorizedException();

            if (order.Status == OrderStatus.Shipped)
                throw new InvalidOrderStateException("Cannot update shipped order.");

            order.ProductId = dto.ProductId;
            order.Quantity = dto.Quantity;
            order.LastUpdatedAt = DateTime.UtcNow;
            
            await _context.SaveChangesAsync();
            return order;
        }

        // cancel order
        // somehow send request to controller. this action should only be possible if order is not shipped
        public async Task<OrderModel> CancelOrderAsync(Guid orderId, int userId)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order == null)
                throw new OrderNotFoundException(orderId);

            if (order.UserId != userId)
                throw new UserNotAuthorizedException();

            if (order.Status == OrderStatus.Shipped)
                throw new InvalidOrderStateException("Cannot cancel shipped order.");

            if (order.Status == OrderStatus.Cancelled)
                return order;

            order.Status = OrderStatus.Cancelled;
            order.LastUpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return order;
        }
    }
}
