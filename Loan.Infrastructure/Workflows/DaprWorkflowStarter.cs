using Dapr.Workflow;
using Loan.UseCases.Commands;

namespace Loan.Infrastructure.Workflows
{
    public class DaprWorkflowStarter : IWorkflowStarter
    {
        private readonly DaprWorkflowClient _client;

        public DaprWorkflowStarter(DaprWorkflowClient client)
        {
            _client = client;
        }
        public async Task StartBookLoanWorkflowAsync(Guid loanId, Guid bookId, Guid userId)
        {
            var input = new BookLoanInput
        }
    }
}
