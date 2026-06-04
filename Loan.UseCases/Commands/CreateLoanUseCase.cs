using Loan.UseCases.Repositories;
using Loan.Facade.Interfaces;
using Loan.Facade.Dtos;
using Loan.Domain.Entities;

namespace Loan.UseCases.Commands
{
    public class CreateLoanUseCase : ICreateLoanUseCase
    {
        private readonly ILoanRepository _repo;
        private readonly IWorkflowStarter _workflowStarter;

        public CreateLoanUseCase(ILoanRepository repo, IWorkflowStarter workflowStarter)
        {
            _repo = repo;
            _workflowStarter = workflowStarter;
        }

        public async Task Execute(CreateBookLoanRequestDto request)
        {
            var bookLoan = new BookLoan(request.BookId, request.UserId);
            await _repo.AddAsync(bookLoan);
            await _repo.SaveAsync();

            await _workflowStarter.StartBookLoanWorkflowAsync(bookLoan.Id, bookLoan.BookId, bookLoan.UserId);
        }
    }
}

public interface IWorkflowStarter 
{ 
    Task StartBookLoanWorkflowAsync(Guid loanId, Guid bookId, Guid userId); 
}