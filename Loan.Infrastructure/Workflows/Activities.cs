using Dapr.Workflow;
using Loan.UseCases.Ports;
using Loan.UseCases.Repositories;

namespace Loan.Infrastructure.Workflows;

public class ReserveBookActivity(IInventoryService service) : WorkflowActivity<BookLoanInput, bool>
{
    public override async Task<bool> RunAsync(WorkflowActivityContext context,  BookLoanInput input)
    {
        return await service.ReserveBookAsync(input.LoanId, input.BookId);
    }
}

public class ReleaseBookActivity(IInventoryService service) : WorkflowActivity<BookLoanInput, object?>
{
    public override async Task<object?> RunAsync(WorkflowActivityContext context, BookLoanInput input)
    {
        await service.ReleaseBookAsync(input.LoanId, input.BookId);
        return null;
    }
}

public class ConfirmLoanActivity(ILoanRepository repo) : WorkflowActivity<BookLoanInput, object?>
{
    public override async Task<object?> RunAsync(WorkflowActivityContext context, BookLoanInput input)
    {
        var loan = await repo.GetByIdAsync(input.LoanId) ?? throw new Exception("Book not found");
        loan.Confirm();
        await repo.SaveAsync();
        return null;
    }
}

public class CancelLoanActivity(ILoanRepository repo, ILoanEventPublisher pub) : WorkflowActivity<CancelInput, object?>
{
    public override async Task<object?> RunAsync(WorkflowActivityContext context, CancelInput input)
    {
        var loan = await repo.GetByIdAsync(input.LoanId);
        if (loan is not null)
        {
            loan.Cancel(input.reason);
            await repo.SaveAsync();
            return null;
        }
    }
}   