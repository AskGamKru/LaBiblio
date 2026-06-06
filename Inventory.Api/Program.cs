using Dapr;
using Inventory.Api.Subscribers;
using Inventory.Infrastructure.Persistence;
using Inventory.Infrastructure.Repositories;
using Inventory.UseCases.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Routing;
using Inventory.Facade.Interfaces;
using Inventory.UseCases.Commands;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.

// DbContext
builder.Services.AddDbContext<InventoryDbContext>(options => 
    options.UseNpgsql(builder.Configuration.GetConnectionString("inventoryDb")));

builder.Services.AddScoped<IBookInventoryRepository, BookInventoryRepository>();
builder.Services.AddScoped<IReserveBookUseCase, ReserveBookUseCase>();
builder.Services.AddScoped<IReleaseBookUseCase, ReleaseBookUseCase>();
builder.Services.AddDaprClient();


builder.Services.AddControllers()
    .AddApplicationPart(typeof(BookCreatedSubscriberInv).Assembly)
    .AddDapr();

builder.Services.AddOpenApi();

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// Kør migrations automatisk
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<InventoryDbContext>();

    db.Database.Migrate();
}

app.UseCloudEvents();               // Til model-binding, så DTO virker.

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.MapSubscribeHandler();          // Registrering af [Topic] subscriptions hos Dapr.

var endpointSource = app.Services.GetRequiredService<EndpointDataSource>();

//app.UseHttpsRedirection();

app.Run();
