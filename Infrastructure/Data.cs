using Microsoft.EntityFrameworkCore;
using OrderFlowApi.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) 
        : base(options)
    {
    }

    public DbSet<OrderModel> Orders { get; set; }   
    public DbSet<ProductModel> Products { get; set; }   
}