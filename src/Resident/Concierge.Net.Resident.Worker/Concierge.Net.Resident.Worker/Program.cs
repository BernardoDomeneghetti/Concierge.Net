using Concierge.Net.Resident.Domain.Residents;
using Concierge.Net.Resident.Worker;
using Concierge.Net.Resident.Worker.Application.Abstractions.Context;
using Concierge.Net.Resident.Worker.Application.Abstractions.Messaging;
using Concierge.Net.Resident.Worker.Application.Abstractions.Repositories;
using Concierge.Net.Resident.Worker.Application.Abstractions.Services;
using Concierge.Net.Resident.Worker.Application.Context;
using Concierge.Net.Resident.Worker.Application.Decorators;
using Concierge.Net.Resident.Worker.Application.Residents.Handlers;
using Concierge.Net.Resident.Worker.Application.Services;
using Concierge.Net.Resident.Worker.Infraestructure.Messaging;
using Concierge.Net.Resident.Worker.Infraestructure.Outbox;
using Concierge.Net.Resident.Worker.Infraestructure.Persistence;
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

// Cross-cutting & Notification Services
builder.Services.AddSingleton<ICorrelationContext, CorrelationContext>();
builder.Services.AddScoped<IEmailService, FakeEmailService>();
builder.Services.AddScoped<ISmsService, FakeSmsService>();
builder.Services.AddScoped<IPushNotificationService, FakePushNotificationService>();
builder.Services.AddScoped<IResidentNotificationService, ResidentNotificationService>();
builder.Services.AddScoped<IMessageBus, InMemoryMessageBus>();
builder.Services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();
builder.Services.AddScoped<OutboxProcessor>();
builder.Services.AddHostedService<OutboxBackgroundWorker>();

// Domain Event Handlers (with Logging Decorator)
builder.Services.AddScoped<ResidentRegisteredEventHandler>();
builder.Services.AddScoped<IDomainEventHandler<ResidentRegisteredDomainEvent>>(sp =>
    new DomainEventHandlerLoggingDecorator<ResidentRegisteredDomainEvent>(
        sp.GetRequiredService<ResidentRegisteredEventHandler>(),
        sp.GetRequiredService<ILogger<DomainEventHandlerLoggingDecorator<ResidentRegisteredDomainEvent>>>(),
        sp.GetRequiredService<ICorrelationContext>()));

builder.Services.AddScoped<ResidentUpdatedEventHandler>();
builder.Services.AddScoped<IDomainEventHandler<ResidentUpdatedDomainEvent>>(sp =>
    new DomainEventHandlerLoggingDecorator<ResidentUpdatedDomainEvent>(
        sp.GetRequiredService<ResidentUpdatedEventHandler>(),
        sp.GetRequiredService<ILogger<DomainEventHandlerLoggingDecorator<ResidentUpdatedDomainEvent>>>(),
        sp.GetRequiredService<ICorrelationContext>()));

builder.Services.AddScoped<ResidentInactivatedEventHandler>();
builder.Services.AddScoped<IDomainEventHandler<ResidentInactivatedDomainEvent>>(sp =>
    new DomainEventHandlerLoggingDecorator<ResidentInactivatedDomainEvent>(
        sp.GetRequiredService<ResidentInactivatedEventHandler>(),
        sp.GetRequiredService<ILogger<DomainEventHandlerLoggingDecorator<ResidentInactivatedDomainEvent>>>(),
        sp.GetRequiredService<ICorrelationContext>()));

var host = builder.Build();
host.Run();
