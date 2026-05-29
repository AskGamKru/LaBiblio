using Dapr;
using Catalog.UseCases.Repositories;
using Microsoft.AspNetCore.Mvc;
using Catalog.Domain.Entities;

namespace Catalog.Api.Subscribers
{
    [ApiController]
    [Route ("api/books")]
    public class BookCreatedSubscriber : ControllerBase
    {
        private readonly IBookRepository _repo;

        public BookCreatedSubscriber(IBookRepository repo)
        {
            _repo = repo;
        }

        [Topic("labiblio-pubsub", "book-created")]
        [HttpPost("book-created")]

        public async Task<IActionResult> Handle (BookCreatedMessage msg)
        {
            // Logging
            Console.WriteLine("EVENT RECEIVED");
            Console.WriteLine(msg.Title);

            var book = new Book(
                msg.Title,
                msg.Author
                );

            //Logging
            Console.WriteLine("SAVING BOOK");

            await _repo.AddAsync(book);
            await _repo.SaveAsync();

            //Logging
            Console.WriteLine("BOOK SAVED");

            return Ok();
        }
    }
    public record BookCreatedMessage(Guid BookId, string Title, string Author);
}
