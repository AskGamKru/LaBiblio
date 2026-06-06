using Inventory.Domain.Entities;
namespace Inventory.UseCases.Repositories
{
    public interface IBookInventoryRepository
    {
        Task AddAsync(BookInventory book);
        Task SaveAsync();
        Task<BookInventory?> GetByBookIdAsync(Guid bookId);
    }
}
