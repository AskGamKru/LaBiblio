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

var catalogDb = postgres.AddDatabase("catalogDb");
var inventoryDb = postgres.AddDatabase("inventoryDb");
var loanDb = postgres.AddDatabase("loanDb");

// Umbraco
builder.AddProject<Projects.LaBiblio_Um>("laBiblio-um")
    .WithDaprSidecar(new DaprSidecarOptions
    {
        AppId = "um",                                   // Identitet Dapr bruger til service discovery og pub/sub.
        DaprHttpPort = 5001,
        ResourcesPaths = daprResources
    });


// Catalog
var catalog = builder.AddProject<Projects.Catalog_Api>("catalog-api")
    .WithReference(catalogDb)                           // Giver adgang til databasen.
    .WaitFor(catalogDb)                                 // Projektet starter først efter adgang til db er sikret = ingen race conditions.
    .WithDaprSidecar(new DaprSidecarOptions
    {
        AppId = "catalog",                              // Identitet Dapr bruger til service discovery og pub/sub.
        DaprHttpPort = 5002,
        ResourcesPaths = daprResources,
        AppHealthCheckPath = "/health",
        AppHealthThreshold = 3
    });

// Inventory
var inventory = builder.AddProject<Projects.Inventory_Api>("inventory-api")
    .WithReference(inventoryDb)
    .WaitFor(inventoryDb)
    .WithDaprSidecar(new DaprSidecarOptions
    {
        AppId = "inventory",
        DaprHttpPort = 5003,
        ResourcesPaths = daprResources
    });

// loan
var loan = builder.AddProject<Projects.Loan_Api>("loan-api")
    .WithReference(loanDb)
    .WaitFor(loanDb)
    .WithDaprSidecar(new DaprSidecarOptions
    {
        AppId = "loan",
        DaprHttpPort = 5004,
        ResourcesPaths = daprResources
    });

builder.Build().Run();