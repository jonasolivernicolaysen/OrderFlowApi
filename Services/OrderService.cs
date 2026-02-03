using Microsoft.AspNetCore.Http.HttpResults;
using OrderFlowApi.Models.DTOs;
using OrderFlowApi.Models;
using OrderFlowApi.Mappers;
using OrderFlowApi.User;
using OrderFlowApi.Exceptions;
using Microsoft.EntityFrameworkCore;


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

            // check if product exists
            var product = await GetProductOrThrowAsync(dto.ProductId);

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

        // pay for order
        public async Task<PaymentModel> PayForOrderAsync(Guid orderId, int accountNumber, int userId)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order == null)
                throw new OrderNotFoundException(orderId);

            if (order.UserId != userId)
                throw new UserNotAuthorizedException();

            var product = await _context.Products.FindAsync(order.ProductId);
            if (product == null)
                throw new ProductNotFoundException(order.ProductId);
          
            var totalCost = product.Price * order.Quantity;

            if (order.Status == OrderStatus.Paid)
            {
                return await _context.Payments.FirstAsync(p => p.OrderId == orderId);
            }

            if (order.Status != OrderStatus.Pending)
                throw new InvalidOrderStateException("Only pending orders can be paid for.");

            var payment = PaymentMapper.ToPaymentModel(orderId, accountNumber, 999999, PaymentStatus.Completed);

            using var transaction = await _context.Database.BeginTransactionAsync();

            _context.Payments.Add(payment);
            order.Status = OrderStatus.Paid;
            order.LastUpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
            return payment;
        }

        // check if id exists
        public async Task<ProductModel?> GetProductOrThrowAsync(Guid productId)
        {
            return await _context.Products.FindAsync(productId) ?? throw new ProductNotFoundException(productId);
        }
    }
}
