using Microsoft.AspNetCore.Http.HttpResults;
using OrderFlowApi.Models.DTOs;
using OrderFlowApi.Models;


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
        public async Task<OrderModel> CreateOrderAsync(OrderModel dto)
        {
            _context.Orders.Add(dto);
            await _context.SaveChangesAsync();
            return dto;
        }

        // get order by order id
        public async Task<OrderModel?> GetOrderByIdAsync(int OrderId)
        {
            return await _context.Orders.FindAsync(OrderId);
        }

        // update order if possible
        // this action should only be possible if order is not shipped
        public async Task<OrderModel> UpdateOrderAsync(int OrderId, OrderModel dto)
        {
            var order = await _context.Orders.FindAsync(OrderId);
            if (order == null)
                throw new Exception("Order not found");
            
            _context.Orders.Update(dto);
            await _context.SaveChangesAsync();
            return dto;
        }

        // cancel order
        // somehow send request to controller. this action should only be possible if order is not shipped
    }
}
