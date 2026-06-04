using Loan.Facade.Dtos;

namespace Loan.Facade.Interfaces
{
    public interface ICreateLoanUseCase
    {
        public Task Execute(CreateBookLoanRequestDto request);
    }
}
