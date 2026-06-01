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
            Console.WriteLine($"PUBLISHING: {id} {title}");

            Console.WriteLine($"DAPR_HTTP_ENDPOINT={Environment.GetEnvironmentVariable("DAPR_HTTP_ENDPOINT")}");
            Console.WriteLine($"DAPR_GRPC_ENDPOINT={Environment.GetEnvironmentVariable("DAPR_GRPC_ENDPOINT")}");

            await _dapr.PublishEventAsync("labiblio-pubsub", "book-created", new
            {
                BookId = id, Title = title, Author = author
            });
            Console.WriteLine("PUBLISH COMPLETE!");
        }
    }
}
