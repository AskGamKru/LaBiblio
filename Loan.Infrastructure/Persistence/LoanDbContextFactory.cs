using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Loan.Infrastructure.Persistence
{
    public class LoanDbContextFactory : IDesignTimeDbContextFactory<LoanDbContext>
    {
        public LoanDbContext CreateDbContext(string[] args)
        {
            var options = new DbContextOptionsBuilder<LoanDbContext>();

            options.UseNpgsql(
                "Host=localhost;Port=57320;Username=postgres;Password=S(_+A_jG4cV7uav+KB)m{3;Database=loanDb");

            return new LoanDbContext(options.Options);
        }
    }
}
