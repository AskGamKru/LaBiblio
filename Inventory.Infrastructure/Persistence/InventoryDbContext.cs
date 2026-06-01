using Microsoft.EntityFrameworkCore;
using Inventory.Domain.Entities;
namespace Inventory.Infrastructure.Persistence
{
    public class InventoryDbContext : DbContext
    {
        public DbSet<BookInventory> BookInventories => Set<BookInventory>();

        public InventoryDbContext(DbContextOptions<InventoryDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookInventory>()
                .HasKey(bi => bi.Id);

            modelBuilder.Entity<BookInventory>()
                .HasIndex(bi => bi.BookId).IsUnique();
        }
    }
}
