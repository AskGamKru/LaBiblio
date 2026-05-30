using CommunityToolkit.Aspire.Hosting.Dapr;
using System.Collections.Immutable;
using Aspire.Hosting;

var builder = DistributedApplication.CreateBuilder(args);

// Dapr
var daprResources = ImmutableHashSet.Create("../dapr");

// DB
var postgres = builder.AddPostgres("postgres")          // Oprettelse af en PostgreSQL-container i Docker.
    .WithLifetime(ContainerLifetime.Persistent)         // Behold data mellem runs.
    .WithPgAdmin();                                     // Giver adgang til UI over databasen.

var catalogDb = postgres.AddDatabase("catalogDb");      // Opretter DB i ovennævnte container.

// Umbraco
builder.AddProject<Projects.LaBiblio_Ui>("laBiblio-ui")
    .WithDaprSidecar(new DaprSidecarOptions             // Aktiverer en Dapr sidecar til servicen.
    {
        AppId = "ui",                                   // Identitet Dapr bruger til service discovery og pub/sub.
        DaprHttpPort = 5001,
        ResourcesPaths = daprResources                  // Sti til Dapr-komponenter/configuration.
    });


// Catalog
var catalog = builder.AddProject<Projects.Catalog_Api>("catalog-api")
    .WithReference(catalogDb)                           // Giver adgang til databasen.
    .WaitFor(catalogDb)                                 // Projektet starter først efter adgang til db er sikret = ingen race conditions.
    .WithDaprSidecar(new DaprSidecarOptions             // Aktiverer en Dapr sidecar til servicen.
    {
        AppId = "catalog",                              // Identitet Dapr bruger til service discovery og pub/sub.
        DaprHttpPort = 5002,
        ResourcesPaths = daprResources,                  // Sti til Dapr-komponenter/configuration.
        AppHealthCheckPath = "/health",
        AppHealthThreshold = 3
    });

builder.Build().Run();