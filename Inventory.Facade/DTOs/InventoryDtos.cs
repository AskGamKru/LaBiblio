namespace Inventory.Facade.Dtos;

public record ReserveBookRequestDto(Guid LoanId, Guid BookId);