namespace Loan.UseCases.Ports
{
    public interface IInventoryService
    {
        Task<bool> ReserveBookAsync(Guid loanId, Guid bookId);
        Task ReleaseBookAsync (Guid loanId, Guid bookId);
    }
}
