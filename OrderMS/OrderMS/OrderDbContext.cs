using Microsoft.EntityFrameworkCore;

namespace OrderMS
{
    public class OrderDbContext:DbContext
    {
        public OrderDbContext(DbContextOptions<OrderDbContext> options):base(options)
        {
        
        }

        public DbSet<Order> OrderTb { get; set; }
    }
}
