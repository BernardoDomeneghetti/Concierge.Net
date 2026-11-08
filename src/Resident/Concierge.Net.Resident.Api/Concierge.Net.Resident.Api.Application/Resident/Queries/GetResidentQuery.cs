using Concierge.Net.Resident.Api.Application.Abstractions.Messaging;

namespace Concierge.Net.Resident.Api.Application.Residents.Queries;

public class GetResidentQuery : IQuery
{
    public Guid ResidentId { get; init; }
}
