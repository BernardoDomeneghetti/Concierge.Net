using Concierge.Net.Resident.Api.Application.Abstractions.UseCases;
using Concierge.Net.Resident.Api.Application.Residents.Commands;
using Concierge.Net.Resident.Api.Application.Residents.Queries;
using Concierge.Net.Resident.Api.Contracts;
using Concierge.Net.Resident.Domain.Residents;
using Microsoft.AspNetCore.Mvc;

namespace Concierge.Net.Resident.Api.Controllers;

[ApiController]
[Route("api/residents")]
public class ResidentsController(
    ICommandHandler<RegisterResidentCommand, ResidentId> registerHandler,
    ICommandHandler<UpdateResidentCommand, ResidentId> updateHandler,
    ICommandHandler<InactivateResidentCommand, ResidentId> inactivateHandler,
    IQueryHandler<GetResidentQuery, GetResidentResponse> getHandler) : ControllerBase
{
    private readonly ICommandHandler<RegisterResidentCommand, ResidentId> _registerHandler = registerHandler;
    private readonly ICommandHandler<UpdateResidentCommand, ResidentId> _updateHandler = updateHandler;
    private readonly ICommandHandler<InactivateResidentCommand, ResidentId> _inactivateHandler = inactivateHandler;
    private readonly IQueryHandler<GetResidentQuery, GetResidentResponse> _getHandler = getHandler;

    [HttpPost]
    public async Task<IActionResult> Register(
        [FromBody] RegisterResidentRequest request,
        CancellationToken ct)
    {
        var command = new RegisterResidentCommand
        {
            FullName = request.FullName,
            Cpf = request.Cpf,
            Email = request.Email,
            MobilePhone = request.MobilePhone,
            PhotoUrl = request.PhotoUrl,
            UnitId = request.UnitId,
            ResidentType = request.ResidentType,
            NotificationChannel = request.NotificationChannel
        };

        var residentId = await _registerHandler.ExecuteAsync(command, ct);

        return CreatedAtAction(
            nameof(GetById),
            new { id = residentId.Value },
            new { id = residentId.Value });
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(
        [FromRoute] Guid id,
        [FromBody] UpdateResidentRequest request,
        CancellationToken ct)
    {
        var command = new UpdateResidentCommand
        {
            ResidentId = id,
            FullName = request.FullName,
            Email = request.Email,
            MobilePhone = request.MobilePhone,
            PhotoUrl = request.PhotoUrl,
            UnitId = request.UnitId,
            ResidentType = request.ResidentType,
            NotificationChannel = request.NotificationChannel,
            UpdatedAtUtc = request.UpdatedAtUtc
        };

        var residentId = await _updateHandler.ExecuteAsync(command, ct);

        return Ok(new { id = residentId.Value });
    }

    [HttpPost("{id:guid}/inactivate")]
    public async Task<IActionResult> Inactivate(
        [FromRoute] Guid id,
        [FromBody] InactivateResidentRequest request,
        CancellationToken ct)
    {
        var command = new InactivateResidentCommand
        {
            ResidentId = id,
            Reason = request.Reason,
            InactivatedAtUtc = request.InactivatedAtUtc
        };

        var residentId = await _inactivateHandler.ExecuteAsync(command, ct);

        return Ok(new { id = residentId.Value });
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<GetResidentResponse>> GetById(
        [FromRoute] Guid id,
        CancellationToken ct)
    {
        var response = await _getHandler.ExecuteAsync(new GetResidentQuery { ResidentId = id }, ct);
        return Ok(response);
    }
}
