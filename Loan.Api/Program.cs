using Dapr.Client;
using Dapr.Workflow;
using Loan.Facade.Interfaces;
using Loan.Infrastructure.ExternalServices;
using Loan.Infrastructure.Persistence;
using Loan.Infrastructure.Repositories;
using Loan.Infrastructure.Workflows;
using Loan.UseCases.Commands;
using Loan.UseCases.Repositories;
using Loan.UseCases.Ports;

using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddDbContext<LoanDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("loanDb")));
builder.Services.AddScoped<ILoanRepository, LoanRepository>();
builder.Services.AddScoped<ICreateLoanUseCase, CreateLoanUseCase>();
builder.Services.AddScoped<IInventoryService, DaprInventoryService>();

builder.Services.AddKeyedSingleton<HttpClient>("catalog", (_, _) =>
    DaprClient.CreateInvokeHttpClient("catalog"));
builder.Services.AddKeyedSingleton<HttpClient>("inventory", (_, _) =>
    DaprClient.CreateInvokeHttpClient("inventory"));
builder.Services.AddDaprClient();
builder.Services.AddDaprWorkflow(options =>
{
    options.RegisterWorkflow<BookLoanWorkflow>();
    options.RegisterActivity<ReserveBookActivity>();
    options.RegisterActivity<ReleaseBookActivity>();
});

builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}


app.UseAuthorization();

app.UseCloudEvents();
app.MapControllers();
app.MapSubscribeHandler();

app.Run();
