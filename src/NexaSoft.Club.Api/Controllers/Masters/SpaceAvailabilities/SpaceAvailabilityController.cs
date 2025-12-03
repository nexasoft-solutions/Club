using MediatR;
using Microsoft.AspNetCore.Mvc;
using NexaSoft.Club.Api.Controllers.Masters.SpaceAvailabilities.Request;
using NexaSoft.Club.Application.Masters.SpaceAvailabilities.Commands.CreateSpaceAvailability;
using NexaSoft.Club.Application.Masters.SpaceAvailabilities.Commands.UpdateSpaceAvailability;
using NexaSoft.Club.Application.Masters.SpaceAvailabilities.Commands.DeleteSpaceAvailability;
using NexaSoft.Club.Application.Masters.SpaceAvailabilities.Queries.GetSpaceAvailability;
using NexaSoft.Club.Application.Masters.SpaceAvailabilities.Queries.GetSpaceAvailabilities;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Api.Extensions;
using Microsoft.AspNetCore.Authorization;
using NexaSoft.Club.Api.Attributes;

namespace NexaSoft.Club.Api.Controllers.Masters.SpaceAvailabilities;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class SpaceAvailabilityController(ISender _sender) : ControllerBase
{

    [HttpPost]
    [GeneratePermission]
    [RequirePermission("SpaceAvailability.CreateSpaceAvailability")]
    public async Task<IActionResult> CreateSpaceAvailability(CreateSpaceAvailabilityRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateSpaceAvailabilityCommand(
             request.SpaceId,
             request.DayOfWeek,
             request.StartTime,
             request.EndTime,
             request.CreatedBy
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpPut]
    [GeneratePermission]
    [RequirePermission("SpaceAvailability.UpdateSpaceAvailability")]
    public async Task<IActionResult> UpdateSpaceAvailability(UpdateSpaceAvailabilityRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateSpaceAvailabilityCommand(
             request.Id,
             request.SpaceId,
             request.DayOfWeek,
             request.StartTime,
             request.EndTime,
             request.UpdatedBy
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpDelete]
    [GeneratePermission]
    [RequirePermission("SpaceAvailability.DeleteSpaceAvailability")]
    public async Task<IActionResult> DeleteSpaceAvailability(DeleteSpaceAvailabilityRequest request, CancellationToken cancellationToken)
    {
        var command = new DeleteSpaceAvailabilityCommand(
             request.Id,
             request.DeletedBy
         );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpGet]
    [GeneratePermission]
    [RequirePermission("SpaceAvailability.GetSpaceAvailability")]
    public async Task<IActionResult> GetSpaceAvailabilities(
       [FromQuery] BaseSpecParams specParams,
       CancellationToken cancellationToken
    )
    {
        var query = new GetSpaceAvailabilitiesQuery(specParams);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }


    [HttpGet("{id:long}")]
    [GeneratePermission]
    [RequirePermission("SpaceAvailability.GetSpaceAvailability")]
    public async Task<IActionResult> GetSpaceAvailability(
        long id,
        CancellationToken cancellationToken
     )
    {
        var query = new GetSpaceAvailabilityQuery(id);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }

}
