using Microsoft.EntityFrameworkCore;
using OrderAPI.Data.Models;

namespace OrderAPI.Data
{
    public class OrderAPIContext : DbContext
    {
        public OrderAPIContext(DbContextOptions<OrderAPIContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<CustomerEntity> Customers { get; set; }
        public DbSet<OrderEntity> Order { get; set; }
        public DbSet<OrderItemEntity> OrderItem { get; set; }
    }
}
