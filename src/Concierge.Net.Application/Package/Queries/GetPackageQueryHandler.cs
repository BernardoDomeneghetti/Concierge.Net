using System;
using Concierge.Net.Application.Abstractions.UseCases;

namespace Concierge.Net.Application.Package.Queries;

public class GetPackageQueryHandler: IQueryHandler<GetPackageQuery, GetPackageResponse>
{
    public Task<GetPackageResponse> ExecuteAsync(GetPackageQuery Query, CancellationToken correlationToken)
    {
        throw new NotImplementedException();
    }
}
