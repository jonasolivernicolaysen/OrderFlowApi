using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using OrderFlowApi.Exceptions;
using OrderFlowApi.Models;
using OrderFlowApi.Infrastructure;

namespace OrderFlowApi.Services
{
    public class PaymentService
    {
        private readonly AppDbContext _context;

        public PaymentService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<PaymentModel>> GetPaymentsForOrderAsync(Guid orderId, int userId)
        {
            var order = await _context.Orders.FindAsync(orderId) ?? throw new OrderNotFoundException(orderId);

            if (order.UserId != userId)
                throw new UserNotAuthorizedException();

            return await _context.Payments
                .Where(p => p.OrderId == orderId)
                .OrderBy(p => p.PaidAt)
                .ToListAsync();
        }
    }
}