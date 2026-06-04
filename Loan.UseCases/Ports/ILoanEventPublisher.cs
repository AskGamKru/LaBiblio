namespace Loan.UseCases.Ports
{
    public interface ILoanEventPublisher
    {
        Task PublishLoanConfirmedAsync(Guid loanId, Guid bookId, Guid userId);
        Task PublishLoanCancelledAsync(Guid loanId, Guid bookId, Guid userId, string reason);
    }
}
