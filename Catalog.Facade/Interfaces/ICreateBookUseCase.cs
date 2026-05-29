using Catalog.Facade.DTOs;
namespace Catalog.Facade.Interfaces;

public interface ICreateBookUseCase { Task Execute(CreateBookRequestDto request); }

