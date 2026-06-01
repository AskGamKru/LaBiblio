using Inventory.Infrastructure.Persistence;
using Inventory.UseCases.Repositories;
using Inventory.Domain.Entities;
namespace Inventory.Infrastructure.Repositories
{
    public class BookInventoryRepository : IBookInventoryRepository
    {
        private readonly InventoryDbContext _db;
        public BookInventoryRepository(InventoryDbContext db)
        {
            _db = db;
        }
        public async Task AddAsync(BookInventory bookInventory)
        {
            await _db.BookInventories.AddAsync(bookInventory);
        }
        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
