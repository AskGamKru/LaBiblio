using Dapr.Client;
using Loan.UseCases.Ports;

namespace Loan.Infrastructure.Messaging
{
    public class DaprLoanEventPublisher : ILoanEventPublisher
    {
        private readonly DaprClient _dapr;

        public DaprLoanEventPublisher(DaprClient dapr) => _dapr = dapr;
        
        public async Task PublishLoanConfirmedAsync(Guid loanId, Guid bookId, Guid userId)
        {
            await _dapr.PublishEventAsync("labiblio-pubsub", "loan-confirmed", 
                new 
                { 
                    LoanId = loanId, 
                    BookId = bookId, 
                    UserId = userId 
                });
        }

        public async Task PublishLoanCancelledAsync(Guid loanId, Guid bookId, Guid userId, string reason)
        {
            await _dapr.PublishEventAsync("labiblio-pubsub", "loan-cancelled",
                new
                {
                    LoanId = loanId,
                    BookId = bookId,
                    UserId = userId,
                    Reason = reason
                });
        }
    }
}
