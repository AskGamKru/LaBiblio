namespace Catalog.UseCases.Ports;

public interface IBookPublisher { Task PublishBookCreatedAsync(Guid bookId, string title, string author); }