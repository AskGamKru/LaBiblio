using Catalog.Facade.Interfaces;
using Catalog.Facade.DTOs;
using Catalog.UseCases.Repositories;
using Catalog.UseCases.Ports;

namespace Catalog.UseCases.Commands
{
    public class PublishBookUseCase : IPublishBookUseCase
    {
        private readonly IBookRepository _repo;
        private readonly IBookPublisher _publisher;

        public PublishBookUseCase (IBookRepository repo, IBookPublisher publisher) 
        { _repo = repo; _publisher = publisher; }
        
        
        public async Task Execute (PublishBookRequestDto request)
        {
            var book = await _repo.GetByIdAsync(request.BookId) 
                ?? throw new InvalidOperationException("Book not found.");

            await _repo.SaveAsync();

            await _publisher.PublishBookCreatedAsync(book.Id, book.Title, book.Author);
        }
    }
}
