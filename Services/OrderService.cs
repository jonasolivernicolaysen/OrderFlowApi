using Microsoft.AspNetCore.Http.HttpResults;
using OrderFlowApi.Models.DTOs;
using OrderFlowApi.Models;
using OrderFlowApi.Mappers;


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
            var order = OrderMapper.ToCreateModel(dto, userId);
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return order;
        }

        // get order by order id
        public async Task<OrderModel?> GetOrderByIdAsync(Guid OrderId)
        {
            return await _context.Orders.FindAsync(OrderId);
        }

        // update order if possible
        // this action should only be possible if order is not shipped
        public async Task<OrderModel> UpdateOrderAsync(Guid OrderId, UpdateOrderDto dto, int userId)
        {
            var order = await _context.Orders.FindAsync(OrderId);
            if (order == null)
                throw new Exception("Order not found");

            // if order is shipped
            if (order.Status == OrderStatus.Shipped)
                throw new InvalidOperationException("Cannot update shipped order");

            order.ProductId = dto.ProductId;
            order.Quantity = dto.Quantity;
            order.LastUpdatedAt = DateTime.UtcNow;
            
            await _context.SaveChangesAsync();
            return order;
        }

        // cancel order
        // somehow send request to controller. this action should only be possible if order is not shipped
    }
}
