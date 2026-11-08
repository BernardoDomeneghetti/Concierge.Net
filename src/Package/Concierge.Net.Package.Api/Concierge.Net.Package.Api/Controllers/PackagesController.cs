using Concierge.Net.Package.Api.Application.Abstractions.UseCases;
using Concierge.Net.Package.Api.Application.Packages.Commands;
using Concierge.Net.Package.Api.Application.Packages.Queries;
using Concierge.Net.Package.Domain.Packages;
using Concierge.Net.Package.Api.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Concierge.Net.Package.Api.Controllers;

[ApiController]
[Route("api/packages")]
public class PackagesController(
    ICommandHandler<ReceivePackageCommand, PackageId> receiveHandler,
    ICommandHandler<CollectPackageCommand, PackageId> collectHandler,
    ICommandHandler<CancelPackageCommand, PackageId> cancelHandler,
    IQueryHandler<GetPackageQuery, GetPackageResponse> getHandler) : ControllerBase
{
    private readonly ICommandHandler<ReceivePackageCommand, PackageId> _receiveHandler = receiveHandler;
    private readonly ICommandHandler<CollectPackageCommand, PackageId> _collectHandler = collectHandler;
    private readonly ICommandHandler<CancelPackageCommand, PackageId> _cancelHandler = cancelHandler;
    private readonly IQueryHandler<GetPackageQuery, GetPackageResponse> _getHandler = getHandler;

    [HttpPost]
    public async Task<IActionResult> Receive(
        [FromBody] ReceivePackageRequest request,
        CancellationToken ct)
    {
        var command = new ReceivePackageCommand
        {
            ResidentId = request.ResidentId,
            UnitId = request.UnitId,
            Email = request.Email
        };

        var packageId = await _receiveHandler.ExecuteAsync(command, ct);

        return CreatedAtAction(
            nameof(GetById),
            new { id = packageId.Value },
            new { id = packageId.Value });
    }

    [HttpPost("{id:guid}/collect")]
    public async Task<IActionResult> Collect(
        [FromRoute] Guid id,
        [FromBody] CollectPackageRequest request,
        CancellationToken ct)
    {
        var command = new CollectPackageCommand
        {
            PackageId = id,
            CollectedByResidentId = request.CollectedByResidentId,
            SignatureHash = request.SignatureHash,
            Email = request.Email,
            CollectedAtUtc = request.CollectedAtUtc
        };

        var packageId = await _collectHandler.ExecuteAsync(command, ct);

        return Ok(new { id = packageId.Value });
    }

    [HttpPost("{id:guid}/cancel")]
    public async Task<IActionResult> Cancel(
        [FromRoute] Guid id,
        [FromBody] CancelPackageRequest request,
        CancellationToken ct)
    {
        var command = new CancelPackageCommand
        {
            PackageId = id,
            Reason = request.Reason,
            Email = request.Email,
            CancelledAtUtc = request.CancelledAtUtc
        };

        var packageId = await _cancelHandler.ExecuteAsync(command, ct);

        return Ok(new { id = packageId.Value });
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<GetPackageResponse>> GetById(
        [FromRoute] Guid id,
        CancellationToken ct)
    {
        var response = await _getHandler.ExecuteAsync(new GetPackageQuery { PackageId = id }, ct);
        return Ok(response);
    }
}
