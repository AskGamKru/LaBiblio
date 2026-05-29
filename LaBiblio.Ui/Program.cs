using LaBiblio.Ui.Notifications;
using Umbraco.Cms.Core.Notifications;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddDaprClient();
builder.Services.AddScoped<BookCreatedPublisher>();

/*
builder.Services.AddHttpClient("catalog", client =>
{
    client.BaseAddress = new Uri("http://localhost:5001/v1.0/invoke/catalog/method/");
});
*/

builder.CreateUmbracoBuilder()
    .AddBackOffice()
    .AddWebsite()
    .AddNotificationAsyncHandler<ContentPublishedNotification, BookPublishedHandler>()
    .AddComposers()
    .Build();

WebApplication app = builder.Build();

await app.BootUmbracoAsync();

app.UseHttpsRedirection();

app.UseUmbraco()
    .WithMiddleware(u =>
    {
        u.UseBackOffice();
        u.UseWebsite();
    })
    .WithEndpoints(u =>
    {
        u.UseBackOfficeEndpoints();
        u.UseWebsiteEndpoints();
    });

await app.RunAsync();
