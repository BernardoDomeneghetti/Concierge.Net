using Concierge.Net.Resident.Api.Application.Context;

namespace Concierge.Net.Resident.Api.Middleware;

public sealed class CorrelationIdMiddleware(RequestDelegate next)
{
    public const string HeaderName = "X-Correlation-Id";
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        if (!context.Request.Headers.TryGetValue(HeaderName, out var headerValue) ||
            !Guid.TryParse(headerValue, out var correlationId))
        {
            correlationId = Guid.NewGuid();
        }

        CorrelationContext.Set(correlationId);
        context.Response.Headers[HeaderName] = correlationId.ToString();

        await _next(context);
    }
}
