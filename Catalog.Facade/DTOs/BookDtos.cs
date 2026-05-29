namespace Catalog.Facade.DTOs;

// Command Request DTOs
public record CreateBookRequestDto (string Title, string Author);
public record PublishBookRequestDto(Guid BookId);