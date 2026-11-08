using Concierge.Net.Package.Worker.Application.Abstractions.Context;
using Concierge.Net.Package.Worker.Application.Abstractions.Messaging;
using Concierge.Net.Package.Worker.Application.Abstractions.Repositories;
using Concierge.Net.Package.Worker.Application.Abstractions.Services;
using Concierge.Net.Package.Worker.Application.Context;
using Concierge.Net.Package.Worker.Application.Decorators;
using Concierge.Net.Package.Worker.Application.Packages.Handlers;
using Concierge.Net.Package.Worker.Application.Services;
using Concierge.Net.Package.Domain.Packages;
using Concierge.Net.Package.Worker.Infraestructure.Messaging;
using Concierge.Net.Package.Worker.Infraestructure.Outbox;
using Concierge.Net.Package.Worker.Infraestructure.Persistence;
using Concierge.Net.Package.Worker;
using MongoDB.Driver;

MongoBsonConfiguration.Configure();

var builder = Host.CreateApplicationBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddSimpleConsole(options =>
{
    options.IncludeScopes = true;
    options.TimestampFormat = "HH:mm:ss ";
});

// MongoDB & Outbox Persistence
var mongoConnectionString = builder.Configuration.GetConnectionString("MongoDb")!;
var mongoDatabaseName = builder.Configuration["MongoDb:DatabaseName"]!;

builder.Services.AddSingleton<IMongoClient>(_ => new MongoClient(mongoConnectionString));
builder.Services.AddScoped<IMongoDatabase>(sp =>
    sp.GetRequiredService<IMongoClient>().GetDatabase(mongoDatabaseName));

builder.Services.AddScoped<IOutboxRepository, OutboxRepository>();

// Cross-cutting & Messaging Services
builder.Services.AddSingleton<ICorrelationContext, CorrelationContext>();
builder.Services.AddScoped<IEmailService, FakeEmailService>();
builder.Services.AddScoped<IMessageBus, InMemoryMessageBus>();
builder.Services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();
builder.Services.AddScoped<OutboxProcessor>();
builder.Services.AddHostedService<OutboxBackgroundWorker>();

// Domain Event Handlers (with Logging Decorator)
builder.Services.AddScoped<PackageReceivedEventHandler>();
builder.Services.AddScoped<IDomainEventHandler<PackageReceivedDomainEvent>>(sp =>
    new DomainEventHandlerLoggingDecorator<PackageReceivedDomainEvent>(
        sp.GetRequiredService<PackageReceivedEventHandler>(),
        sp.GetRequiredService<ILogger<DomainEventHandlerLoggingDecorator<PackageReceivedDomainEvent>>>(),
        sp.GetRequiredService<ICorrelationContext>()));

builder.Services.AddScoped<PackageCollectedEventHandler>();
builder.Services.AddScoped<IDomainEventHandler<PackageCollectedDomainEvent>>(sp =>
    new DomainEventHandlerLoggingDecorator<PackageCollectedDomainEvent>(
        sp.GetRequiredService<PackageCollectedEventHandler>(),
        sp.GetRequiredService<ILogger<DomainEventHandlerLoggingDecorator<PackageCollectedDomainEvent>>>(),
        sp.GetRequiredService<ICorrelationContext>()));

builder.Services.AddScoped<PackageCancelledEventHandler>();
builder.Services.AddScoped<IDomainEventHandler<PackageCancelledDomainEvent>>(sp =>
    new DomainEventHandlerLoggingDecorator<PackageCancelledDomainEvent>(
        sp.GetRequiredService<PackageCancelledEventHandler>(),
        sp.GetRequiredService<ILogger<DomainEventHandlerLoggingDecorator<PackageCancelledDomainEvent>>>(),
        sp.GetRequiredService<ICorrelationContext>()));

var host = builder.Build();
host.Run();
