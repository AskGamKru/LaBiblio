using Catalog.Facade.DTOs;

namespace Catalog.Facade.Interfaces;

public interface IPublishBookUseCase { Task Execute(PublishBookRequestDto request); }
