namespace Loan.Facade.Dtos;

public record CreateBookLoanRequestDto(Guid BookId, Guid UserId);

public record BookLoanDto(Guid Id, Guid BookId, Guid UserId, string status, DateTime LoanedAt);