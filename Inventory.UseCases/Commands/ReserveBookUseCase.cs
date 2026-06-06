using Inventory.Domain.Exceptions;
using Inventory.Facade.Dtos;
using Inventory.Facade.Interfaces;
using Inventory.UseCases.Repositories;
using LaBiblio.ServiceDefaults;

namespace Inventory.UseCases.Commands
{
    public class ReserveBookUseCase : IReserveBookUseCase
    {
        private readonly IBookInventoryRepository _repo;

        public ReserveBookUseCase(IBookInventoryRepository repo)
        {
            _repo = repo;
        }
        public async Task<Result> Execute(ReserveBookRequestDto request)
        {
            var inventory = await _repo.GetByBookIdAsync(request.BookId);

            if (inventory == null)
            {
                return Result.Failure("No inventory for this book.");
            }

            try
            {
                inventory.Reserve();
                
                await _repo.SaveAsync();
               
                return Result.Success();
            }
            catch (DomainException ex)
            {
                return Result.Failure(ex.Message);
            }
        }
    }
}
