using Inventory.Infrastructure.Persistence;
using Inventory.UseCases.Repositories;
using Inventory.Domain.Entities;
using Microsoft.EntityFrameworkCore;
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
        public async Task<BookInventory?> GetByBookIdAsync(Guid bookId)
        {
            return await _db.BookInventories.SingleOrDefaultAsync(b => b.BookId == bookId);
        }
    }
}
