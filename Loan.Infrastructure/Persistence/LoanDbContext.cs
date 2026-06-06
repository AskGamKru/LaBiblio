using Loan.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Loan.Infrastructure.Persistence
{
    public class LoanDbContext : DbContext
    {
        public DbSet<BookLoan> BookLoans => Set<BookLoan>();

        public LoanDbContext(DbContextOptions<LoanDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookLoan>(e =>
            {
                e.HasKey(x => x.Id);

                e.Property(x => x.Status)
                    .HasConversion<string>();
            });
        }
    }
}