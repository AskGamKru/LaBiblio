using Inventory.Facade.Dtos;

namespace Inventory.Facade.Interfaces
{
    public interface IReserveBookUseCase
    {
        public Task Execute(ReserveBookRequestDto request);
    }
}
