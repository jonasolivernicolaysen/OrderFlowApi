using Microsoft.EntityFrameworkCore;
using OrderFlowApi.Models;

namespace OrderFlowApi.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) 
            : base(options)
        {
        }

        public DbSet<OrderModel> Orders { get; set; }   
        public DbSet<ProductModel> Products { get; set; }   
        public DbSet<PaymentModel> Payments { get; set; }   
    }
}