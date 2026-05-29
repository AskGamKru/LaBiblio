using Catalog.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using Catalog.Domain.Entities;

using Catalog.UseCases.Repositories;

namespace Catalog.Infrastructure.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly CatalogDbContext _db;

        public BookRepository (CatalogDbContext db)
        {
            _db = db;
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
