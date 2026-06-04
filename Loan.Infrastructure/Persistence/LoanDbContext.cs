using Microsoft.EntityFrameworkCore;
using Loan.Domain.Entities;

namespace Loan.Infrastructure.Persistence
{
    public class LoanDbContext : DbContext
    {
        public DbSet<BookLoan> BookLoans => Set<BookLoan>();

        public LoanDbContext(DbContextOptions<LoanDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookLoan>()
                .HasKey(x => x.Id);
        }
    }
}
