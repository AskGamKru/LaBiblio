using Catalog.UseCases.Ports;
using Dapr.Client; 

namespace Catalog.Infrastructure.Messaging
{
    public class DaprBookPublisher : IBookPublisher
    {
        private readonly DaprClient _dapr;

        public DaprBookPublisher (DaprClient dapr) => _dapr = dapr;

        public async Task PublishBookCreatedAsync(Guid id, string title, string author)
        {
            await _dapr.PublishEventAsync("labiblio-pubsub", "book-created", new
            {
                Id = id, Title = title, Author = author
            });
        }
    }
}
