using Dapr.Workflow;

namespace Loan.Infrastructure.Workflows
{
    public class BookLoanWorkflow : Workflow<BookLoanInput, BookLoanResult>
    {
        public override async Task<BookLoanResult> RunAsync(WorkflowContext context, BookLoanInput input)
        {
            bool bookReserved = false;
            try
            {
                // Trin 1: Reservér bog
                bookReserved = await context.CallActivityAsync<bool>(nameof(ReserveBookActivity), input);
                if (!bookReserved) // Equivalent to (bookReserved == false)
                {
                    await context.CallActivityAsync(nameof(ReleaseBookActivity),
                        new CancelInput(input.LoanId, "Book is not available"));
                    return new BookLoanResult(false, "Book is not available");
                }

                // Trin 2: Bekræftelse
                await context.CallActivityAsync(nameof(ConfirmLoanActivity), input);
                await context.CallActivityAsync(nameof(PublishLoanConfirmedActivity), input);
                return new BookLoanResult(true, "Loan Confirmed");
            }
            catch (Exception ex)
            {
                if (bookReserved) // Equivalent to (bookReserved == true)
                {
                    await context.CallActivityAsync(nameof(ReleaseBookActivity), input);
                }
                    await context.CallActivityAsync(nameof(CancelLoanActivity), new CancelInput(input.LoanId, $"Error: {ex.Message}"));
                    return new BookLoanResult(false, ex.Message);
            }
        }
    }
}

// DTOs
public record BookLoanInput(Guid LoanId, Guid BookId, Guid UserId);
public record BookLoanResult(bool Success, string Message);
public record CancelInput(Guid LoanId, string reason);