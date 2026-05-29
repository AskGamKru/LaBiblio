using Catalog.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Dapr;
using Catalog.UseCases.Repositories;
using Catalog.Infrastructure.Repositories;
using Catalog.Facade.Interfaces;
using Catalog.UseCases.Commands;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();       // Gør det muligt at kalde service, via navnet (catalog), fra andre services.

// Add services to the container.

// DbContext
builder.Services.AddDbContext<CatalogDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("catalogDb")));

builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<ICreateBookUseCase, CreateBookUseCase>();

builder.Services.AddControllers()   // Gør det muligt at bruge [ApiController] og [Route].
    .AddDapr();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();      // Mulighed for at teste API via Swagger UI.

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()     // Tillad alle domæner (kun til udvikling!)
              .AllowAnyMethod()     // Tillad alle HTTP-metoder (GET, POST, etc.)
              .AllowAnyHeader();    // Tillad alle headers
    });
});

var app = builder.Build();

app.MapDefaultEndpoints();          // Aspire-standard endpoints.

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwagger();
    app.UseSwaggerUI();
}

// Kør migrations automatisk
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<CatalogDbContext>();

    db.Database.Migrate();
}

app.UseHttpsRedirection();

app.UseCloudEvents();               // Til model-binding, så DTO virker.

app.UseRouting();                   

app.MapSubscribeHandler();          // Registrering af [Topic] subscriptions hos Dapr.

app.UseAuthorization();

// CORS
app.UseCors("AllowAll");            // Aktiver CORS-middleware

app.MapControllers();               // Mapper alle controllers med [ApiController].

app.Run();
