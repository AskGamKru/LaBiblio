using Catalog.Domain.Entities;
using Catalog.Facade.DTOs;
using Catalog.Facade.Interfaces;
using Catalog.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly CatalogDbContext _catalogDbContext;
        private readonly ICreateBookUseCase _create;
        private readonly IPublishBookUseCase _publish;

        public BooksController(CatalogDbContext catalogDbContext, ICreateBookUseCase create, IPublishBookUseCase publish)
        {
            _catalogDbContext = catalogDbContext;
            _create = create;
            _publish = publish;
        }

        // Create Book.
        [HttpPost]
        public async Task<IActionResult> Create(CreateBookRequestDto request)
        {
            await _create.Execute(request);
            return Created();
        }

        // Publish Book.
        [HttpPut("{id}/publish")]
        public async Task<IActionResult> Publish (Guid id)
        {
            await _publish.Execute(new PublishBookRequestDto(id));
            return NoContent();
        }

        // GET: Book by id.
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBookById(Guid id)
        {
            var book = await _catalogDbContext.Books.FindAsync(id);
            if (book == null) return NotFound();
            return book;
        }

        /*
        // POST: Book.
        [HttpPost]
        public async Task<ActionResult<Book>> PostBook(Book book)
        {
            _catalogDbContext.Books.Add(book);
            await _catalogDbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetBookById), new { id = book.Id }, book);
        }
        */
    }
}
