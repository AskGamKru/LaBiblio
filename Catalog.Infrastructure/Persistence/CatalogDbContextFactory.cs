using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.Infrastructure.Persistence
{
    public class CatalogDbContextFactory : IDesignTimeDbContextFactory<CatalogDbContext>
    {
        public CatalogDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CatalogDbContext>();

            optionsBuilder.UseNpgsql(
                "Host=localhost;Port=5432;Database=catalogDb;Username=postgres;Password=S(_+A_jG4cV7uav+KB)m{3");

            return new CatalogDbContext(optionsBuilder.Options);
        }
    }
}
