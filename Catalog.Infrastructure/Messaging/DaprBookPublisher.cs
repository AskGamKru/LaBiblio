using Catalog.UseCases.Ports;
using Dapr.Client; 

namespace Catalog.Infrastructure.Messaging
{
    public class DaprBookPublisher : IBookPublisher
    {
        private readonly DaprClient _dapr;

        public DaprBookPublisher (DaprClient dapr) => _dapr = dapr;


    }
}
