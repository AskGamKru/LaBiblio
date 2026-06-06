namespace Inventory.Facade.Dtos;

public record ReserveBookRequestDto(Guid LoanId, Guid BookId);

public record ReleaseBookRequestDto(Guid LoanId, Guid BookId);