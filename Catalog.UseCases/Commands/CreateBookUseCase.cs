using Catalog.Domain.Entities;
using Catalog.Facade.DTOs;
using Catalog.Facade.Interfaces;
using Catalog.UseCases.Repositories;
namespace Catalog.UseCases.Commands
{
    public class CreateBookUseCase : ICreateBookUseCase
    {
        private readonly IBookRepository _repo;

        public CreateBookUseCase (IBookRepository repo) => _repo = repo;

        public async Task Execute(CreateBookRequestDto request)
        {
            var book = new Book(request.Title, request.Author);

            await _repo.AddAsync(book);
            await _repo.SaveAsync();

        }
    }
}
