using Concierge.Net.Package.Api.Application.Abstractions.Context;
using Concierge.Net.Package.Api.Application.Abstractions.Repositories;
using Concierge.Net.Package.Api.Application.Abstractions.UseCases;
using Concierge.Net.Package.Api.Application.Context;
using Concierge.Net.Package.Api.Application.Decorators;
using Concierge.Net.Package.Api.Application.Packages.Commands;
using Concierge.Net.Package.Api.Application.Packages.Queries;
using Concierge.Net.Package.Domain.Packages;
using Concierge.Net.Package.Api.Infraestructure.Persistence;
using Concierge.Net.Package.Api.Middleware;
using MongoDB.Driver;

MongoBsonConfiguration.Configure();

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddSimpleConsole(options =>
{
    options.IncludeScopes = true;
    options.TimestampFormat = "HH:mm:ss ";
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

// MongoDB & Persistence
var mongoConnectionString = builder.Configuration.GetConnectionString("MongoDb")!;
var mongoDatabaseName = builder.Configuration["MongoDb:DatabaseName"]!;

builder.Services.AddSingleton<IMongoClient>(_ => new MongoClient(mongoConnectionString));
builder.Services.AddScoped<IMongoDatabase>(sp =>
    sp.GetRequiredService<IMongoClient>().GetDatabase(mongoDatabaseName));

builder.Services.AddScoped<UnitOfWork>();
builder.Services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<UnitOfWork>());
builder.Services.AddScoped<IPackageRepository, PackageRepository>();

// Cross-cutting
builder.Services.AddSingleton<ICorrelationContext, CorrelationContext>();

// Command Handlers (with Logging Decorator)
builder.Services.AddScoped<ReceivePackageCommandHandler>();
builder.Services.AddScoped<ICommandHandler<ReceivePackageCommand, PackageId>>(sp =>
    new CommandHandlerLoggingDecorator<ReceivePackageCommand, PackageId>(
        sp.GetRequiredService<ReceivePackageCommandHandler>(),
        sp.GetRequiredService<ILogger<CommandHandlerLoggingDecorator<ReceivePackageCommand, PackageId>>>(),
        sp.GetRequiredService<ICorrelationContext>()));

builder.Services.AddScoped<CollectPackageCommandHandler>();
builder.Services.AddScoped<ICommandHandler<CollectPackageCommand, PackageId>>(sp =>
    new CommandHandlerLoggingDecorator<CollectPackageCommand, PackageId>(
        sp.GetRequiredService<CollectPackageCommandHandler>(),
        sp.GetRequiredService<ILogger<CommandHandlerLoggingDecorator<CollectPackageCommand, PackageId>>>(),
        sp.GetRequiredService<ICorrelationContext>()));

builder.Services.AddScoped<CancelPackageCommandHandler>();
builder.Services.AddScoped<ICommandHandler<CancelPackageCommand, PackageId>>(sp =>
    new CommandHandlerLoggingDecorator<CancelPackageCommand, PackageId>(
        sp.GetRequiredService<CancelPackageCommandHandler>(),
        sp.GetRequiredService<ILogger<CommandHandlerLoggingDecorator<CancelPackageCommand, PackageId>>>(),
        sp.GetRequiredService<ICorrelationContext>()));

// Query Handlers (with Logging Decorator)
builder.Services.AddScoped<GetPackageQueryHandler>();
builder.Services.AddScoped<IQueryHandler<GetPackageQuery, GetPackageResponse>>(sp =>
    new QueryHandlerLoggingDecorator<GetPackageQuery, GetPackageResponse>(
        sp.GetRequiredService<GetPackageQueryHandler>(),
        sp.GetRequiredService<ILogger<QueryHandlerLoggingDecorator<GetPackageQuery, GetPackageResponse>>>(),
        sp.GetRequiredService<ICorrelationContext>()));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseExceptionHandler();
app.UseMiddleware<CorrelationIdMiddleware>();

app.MapControllers();

app.Run();
