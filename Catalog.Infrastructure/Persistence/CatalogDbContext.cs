using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Catalog.Domain.Entities;

namespace Catalog.Infrastructure.Persistence
{
    public class CatalogDbContext : DbContext
    {
        public DbSet<Book> Books => Set<Book>();

        public CatalogDbContext(DbContextOptions<CatalogDbContext> options): base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                .HasKey(b => b.Id);
        }
    }
}
