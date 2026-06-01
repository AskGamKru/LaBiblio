using Inventory.Domain.Entities;
namespace Inventory.UseCases.Repositories
{
    public interface IBookInventoryRepository
    {
        Task AddAsync(BookInventory book);
        Task SaveAsync();
    }
}
