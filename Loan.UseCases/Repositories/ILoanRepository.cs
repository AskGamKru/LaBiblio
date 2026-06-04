using Loan.Domain.Entities;

namespace Loan.UseCases.Repositories
{
    public interface ILoanRepository
    {
        public Task<BookLoan?> GetByIdAsync(Guid id);

        public Task AddAsync(BookLoan bookLoan);

        public Task SaveAsync();
    }
}
