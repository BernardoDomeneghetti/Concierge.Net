using System;
using Concierge.Net.Package.Api.Application.Abstractions.Messaging;

namespace Concierge.Net.Package.Api.Application.Packages.Queries;

public class GetPackageQuery: IQuery
{
    public Guid PackageId { get; init; }
}
