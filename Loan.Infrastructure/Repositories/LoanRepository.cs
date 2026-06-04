using Loan.UseCases.Repositories;
using Loan.Infrastructure.Persistence;
using Loan.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Loan.Infrastructure.Repositories
{
    public class LoanRepository : ILoanRepository
    {
        private readonly LoanDbContext _db;

        public LoanRepository(LoanDbContext db) => _db = db;

        public async Task<BookLoan?> GetByIdAsync(Guid id)
        {
            return await _db.BookLoans.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task AddAsync(BookLoan bookLoan)
        {
            await _db.BookLoans.AddAsync(bookLoan);
        }
        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
