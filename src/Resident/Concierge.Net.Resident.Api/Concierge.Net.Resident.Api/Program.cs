using System.Text.Json.Serialization;
using Concierge.Net.Resident.Api.Application.Abstractions.Context;
using Concierge.Net.Resident.Api.Application.Abstractions.Repositories;
using Concierge.Net.Resident.Api.Application.Abstractions.UseCases;
using Concierge.Net.Resident.Api.Application.Context;
using Concierge.Net.Resident.Api.Application.Decorators;
using Concierge.Net.Resident.Api.Application.Residents.Commands;
using Concierge.Net.Resident.Api.Application.Residents.Queries;
using Concierge.Net.Resident.Api.Infraestructure.Persistence;
using Concierge.Net.Resident.Api.Middleware;
using Concierge.Net.Resident.Domain.Residents;
using MongoDB.Driver;

MongoBsonConfiguration.Configure();

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddSimpleConsole(options =>
{
    options.IncludeScopes = true;
    options.TimestampFormat = "HH:mm:ss ";
});

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

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
builder.Services.AddScoped<IResidentRepository, ResidentRepository>();

// Cross-cutting
builder.Services.AddSingleton<ICorrelationContext, CorrelationContext>();

// Command Handlers (with Logging Decorator)
builder.Services.AddScoped<RegisterResidentCommandHandler>();
builder.Services.AddScoped<ICommandHandler<RegisterResidentCommand, ResidentId>>(sp =>
    new CommandHandlerLoggingDecorator<RegisterResidentCommand, ResidentId>(
        sp.GetRequiredService<RegisterResidentCommandHandler>(),
        sp.GetRequiredService<ILogger<CommandHandlerLoggingDecorator<RegisterResidentCommand, ResidentId>>>(),
        sp.GetRequiredService<ICorrelationContext>()));

builder.Services.AddScoped<UpdateResidentCommandHandler>();
builder.Services.AddScoped<ICommandHandler<UpdateResidentCommand, ResidentId>>(sp =>
    new CommandHandlerLoggingDecorator<UpdateResidentCommand, ResidentId>(
        sp.GetRequiredService<UpdateResidentCommandHandler>(),
        sp.GetRequiredService<ILogger<CommandHandlerLoggingDecorator<UpdateResidentCommand, ResidentId>>>(),
        sp.GetRequiredService<ICorrelationContext>()));

builder.Services.AddScoped<InactivateResidentCommandHandler>();
builder.Services.AddScoped<ICommandHandler<InactivateResidentCommand, ResidentId>>(sp =>
    new CommandHandlerLoggingDecorator<InactivateResidentCommand, ResidentId>(
        sp.GetRequiredService<InactivateResidentCommandHandler>(),
        sp.GetRequiredService<ILogger<CommandHandlerLoggingDecorator<InactivateResidentCommand, ResidentId>>>(),
        sp.GetRequiredService<ICorrelationContext>()));

// Query Handlers (with Logging Decorator)
builder.Services.AddScoped<GetResidentQueryHandler>();
builder.Services.AddScoped<IQueryHandler<GetResidentQuery, GetResidentResponse>>(sp =>
    new QueryHandlerLoggingDecorator<GetResidentQuery, GetResidentResponse>(
        sp.GetRequiredService<GetResidentQueryHandler>(),
        sp.GetRequiredService<ILogger<QueryHandlerLoggingDecorator<GetResidentQuery, GetResidentResponse>>>(),
        sp.GetRequiredService<ICorrelationContext>()));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseExceptionHandler();
app.UseMiddleware<CorrelationIdMiddleware>();

app.MapControllers();

app.Run();
