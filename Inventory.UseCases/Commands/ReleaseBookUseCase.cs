using Inventory.Facade.Dtos;
using Inventory.Facade.Interfaces;
using Inventory.UseCases.Repositories;

namespace Inventory.UseCases.Commands
{
    public class ReleaseBookUseCase : IReleaseBookUseCase
    {
        private readonly IBookInventoryRepository _repo;

        public ReleaseBookUseCase(IBookInventoryRepository repo)
        {
            _repo = repo;
        }
        public async Task Execute(ReleaseBookRequestDto request)
        {
            var inventory = await _repo.GetByBookIdAsync(request.BookId) ?? throw new InvalidOperationException("Book does not exist.");

            inventory.Release();

            await _repo.SaveAsync();
        }
    }
}
