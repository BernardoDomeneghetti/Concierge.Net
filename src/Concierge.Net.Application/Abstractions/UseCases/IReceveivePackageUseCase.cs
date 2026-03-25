using System;
using Concierge.Net.Domain.Residents;
using Concierge.Net.Domain.Shared;

namespace Concierge.Net.Application.Abstractions.UseCases;

public interface IReceveivePackageUseCase
{
    Task ExecuteAsync(
        ResidentId residentId,
        UnitId unitId,
        Email email,
        CancellationToken correlationToken);
}
