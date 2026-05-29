using LaBiblio.Ui.Notifications;
using Microsoft.Data.Sqlite;
using Umbraco.Cms.Core.Notifications;
using Umbraco.Cms.Persistence.Sqlite;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

var dbPath = Path.Combine(builder.Environment.ContentRootPath, "umbraco", "Data", "Umbraco.sqlite.db");
builder.Configuration.AddInMemoryCollection(new Dictionary<string, string?>
{
    ["ConnectionStrings:umbracoDbDSN"] = $"Data Source={dbPath};Cache=Shared;Foreign Keys=True",
    ["ConnectionStrings:umbracoDbDSN_ProviderName"] = "Microsoft.Data.Sqlite"
});

// Umbraco's PostConfigureAll switches Mode to ReadWrite (file must exist).
// Pre-create the file with WAL mode so the first boot opens it as NotInstalled → shows installer.
if (!File.Exists(dbPath))
{
    Directory.CreateDirectory(Path.GetDirectoryName(dbPath)!);
    var tmp = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
    using (var conn = new SqliteConnection($"Data Source={tmp};Pooling=False"))
    {
        conn.Open();
        using var cmd = conn.CreateCommand();
        cmd.CommandText = "PRAGMA journal_mode = wal;";
        cmd.ExecuteNonQuery();
    }
    File.Copy(tmp, dbPath);
    File.Delete(tmp);
}

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
    .AddUmbracoSqliteSupport()
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
