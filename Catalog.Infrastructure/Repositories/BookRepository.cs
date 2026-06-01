using Catalog.Infrastructure.Persistence;
using Catalog.Domain.Entities;
using Catalog.UseCases.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Infrastructure.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly CatalogDbContext _db;

        public BookRepository (CatalogDbContext db)
        {
            _db = db;
        }
        public async Task<Book?> GetByIdAsync(Guid id)
        {
            return await _db.Books.FirstOrDefaultAsync(b => b.Id == id);
        }
        public async Task AddAsync(Book book)
        {
            await _db.Books.AddAsync(book);
        }
        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
