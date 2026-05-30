using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Notifications;
using LaBiblio.Ui.DTOs;

namespace LaBiblio.Ui.Notifications
{
    public class BookPublishedHandler : INotificationAsyncHandler<ContentPublishedNotification>
    {
        private readonly BookCreatedPublisher _publisher;

        public BookPublishedHandler(BookCreatedPublisher publisher)
        {
            _publisher = publisher;
        }

        public async Task HandleAsync(ContentPublishedNotification notification, CancellationToken cancellationToken)
        {
            foreach (var content in notification.PublishedEntities)
            {
                // Logging
                Console.WriteLine($"[BookPublishedHandler] alias='{content.ContentType.Alias}', name='{content.Name}'");
                
                if (content.ContentType.Alias != "booksEntry")
                    continue;

                var book = new BookCreatedEventDto
                {
                    BookId = Guid.NewGuid(),
                    Title = content.Name,
                    Author = content.GetValue<string>("author")
                };

                await _publisher.PublishBookCreated(book, cancellationToken);
            }
        }
    }
}
