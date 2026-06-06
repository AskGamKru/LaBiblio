using Inventory.Facade.Dtos;

namespace Inventory.Facade.Interfaces
{
    public interface IReleaseBookUseCase
    {
        public Task Execute(ReleaseBookRequestDto request);
    }
}
