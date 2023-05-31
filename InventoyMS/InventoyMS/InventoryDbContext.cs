using Microsoft.EntityFrameworkCore;

namespace InventoyMS
{
    public class InventoryDbContext:DbContext
    {
        public InventoryDbContext(DbContextOptions<InventoryDbContext> options):base(options) 
        {

        }
        public DbSet<Inventory> InventoryTb { get; set; }
    }
}
