using Inventory.Facade.Dtos;
using Inventory.Facade.Interfaces;
using Inventory.UseCases.Repositories;

namespace Inventory.UseCases.Commands
{
    public class ReserveBookUseCase : IReserveBookUseCase
    {
        private readonly IBookInventoryRepository _repo;

        public ReserveBookUseCase (IBookInventoryRepository repo)
        {
            _repo = repo;
        }
        public async Task Execute(ReserveBookRequestDto request)
        {
            var inventory = await _repo.GetByBookIdAsync(request.BookId) ?? throw new InvalidOperationException("Book does not exist.");
            
            inventory.Reserve();
            
            await _repo.SaveAsync();
        }
    }
}
