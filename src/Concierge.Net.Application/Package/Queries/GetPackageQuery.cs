using System;
using Concierge.Net.Application.Abstractions.Messaging;

namespace Concierge.Net.Application.Packages.Queries;

public class GetPackageQuery: IQuery
{
    public Guid PackageId { get; init; }
}
