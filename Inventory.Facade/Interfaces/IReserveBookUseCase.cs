using Inventory.Facade.Dtos;
using LaBiblio.ServiceDefaults;

namespace Inventory.Facade.Interfaces
{
    public interface IReserveBookUseCase
    {
        public Task<Result> Execute(ReserveBookRequestDto request);
    }
}
