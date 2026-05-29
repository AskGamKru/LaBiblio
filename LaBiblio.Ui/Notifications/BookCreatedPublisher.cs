using Dapr.Client;
using System.Net.Http.Json;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Notifications;
using Umbraco.Extensions;
using LaBiblio.Ui.DTOs;

namespace LaBiblio.Ui.Notifications;

public class BookCreatedPublisher
{
    private readonly DaprClient _dapr;

    public BookCreatedPublisher(DaprClient dapr)
    {
        _dapr = dapr;
    }

    public async Task PublishBookCreated(BookCreatedEventDto bookDto, CancellationToken cancellationToken)
    {
        // Logging
        Console.WriteLine("PUBLISH START");
        Console.WriteLine(bookDto.Title);
        Console.WriteLine("DAPR CLIENT EXISTS: " + (_dapr != null));

        await _dapr.PublishEventAsync(
            "labiblio-pubsub",
            "book-created",
            new
            {
                BookId = bookDto.BookId,
                Title = bookDto.Title,
                Author = bookDto.Author
            },
            cancellationToken);

        // Loggin
        Console.WriteLine("PUBLISH DONE");
        await _dapr.PublishEventAsync("labiblio-pubsub", "book-created", new { Test = "HELLO" });
    }
}