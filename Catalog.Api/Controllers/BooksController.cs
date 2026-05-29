using Catalog.Domain.Entities;
using Catalog.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly CatalogDbContext _catalogDbContext;

        public BooksController(CatalogDbContext catalogDbContext)
        {
            _catalogDbContext = catalogDbContext;
        }

        // GET: Book by id.
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBookById(Guid id)
        {
            var book = await _catalogDbContext.Books.FindAsync(id);
            if (book == null) return NotFound();
            return book;
        }

        // POST: Book.
        [HttpPost]
        public async Task<ActionResult<Book>> PostBook(Book book)
        {
            _catalogDbContext.Books.Add(book);
            await _catalogDbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetBookById), new { id = book.Id }, book);
        }
    }
}
