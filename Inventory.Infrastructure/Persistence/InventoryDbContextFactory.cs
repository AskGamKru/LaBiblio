using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Infrastructure.Persistence
{
    public class InventoryDbContextFactory : IDesignTimeDbContextFactory<InventoryDbContext>
    {
        public InventoryDbContext CreateDbContext(string[] args)
        {
            var options = new DbContextOptionsBuilder<InventoryDbContext>();

            options.UseNpgsql(
                "Host=localhost;Port=56622;Username=postgres;Password=S(_+A_jG4cV7uav+KB)m{3;Database=inventoryDb");

            return new InventoryDbContext(options.Options);
        }
    }
}
