using Dapr;
using Microsoft.AspNetCore.Mvc;
using Inventory.UseCases.Repositories;
using Inventory.Domain.Entities;
namespace Inventory.Api.Subscribers
{
    [ApiController]
    [Route("")]
    public class BookCreatedSubscriberInv : ControllerBase
    {
        private readonly IBookInventoryRepository _repo;
        public BookCreatedSubscriberInv(IBookInventoryRepository repo)
        {
            _repo = repo;
        }

        [Topic("labiblio-pubsub", "book-created")]
        [HttpPost("/subscribe/book-created")]
        public async Task<IActionResult> Handle(BookCreatedMessage msg)
        {
            // Logging
            Console.WriteLine("EVENT RECEIVED");
            Console.WriteLine(msg.Title);

            var bookInventory = new BookInventory(
                msg.BookId,
                msg.Title,
                msg.Author,
                1
                );

            //Logging
            Console.WriteLine("SAVING BOOK INVENTORY");

            await _repo.AddAsync(bookInventory);
            await _repo.SaveAsync();

            //Logging
            Console.WriteLine("BOOK INVENTORY SAVED");

            return Ok();
        }
        public record BookCreatedMessage(Guid BookId, string Title, string Author);
    }    
}
