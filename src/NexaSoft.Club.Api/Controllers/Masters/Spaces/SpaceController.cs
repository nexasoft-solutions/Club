using MediatR;
using Microsoft.AspNetCore.Mvc;
using NexaSoft.Club.Api.Controllers.Masters.Spaces.Request;
using NexaSoft.Club.Application.Masters.Spaces.Commands.CreateSpace;
using NexaSoft.Club.Application.Masters.Spaces.Commands.UpdateSpace;
using NexaSoft.Club.Application.Masters.Spaces.Commands.DeleteSpace;
using NexaSoft.Club.Application.Masters.Spaces.Queries.GetSpace;
using NexaSoft.Club.Application.Masters.Spaces.Queries.GetSpaces;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Api.Extensions;

namespace NexaSoft.Club.Api.Controllers.Masters.Spaces;

[Route("api/[controller]")]
[ApiController]
public class SpaceController(ISender _sender) : ControllerBase
{

    [HttpPost]
    public async Task<IActionResult> CreateSpace(CreateSpaceRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateSpaceCommand(
             request.SpaceName,
             request.SpaceTypeId,
             request.Capacity,
             request.Description,
             request.StandardRate,
             request.RequiresApproval,
             request.MaxReservationHours,
             request.IncomeAccountId,
             request.CreatedBy
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateSpace(UpdateSpaceRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateSpaceCommand(
             request.Id,
             request.SpaceName,
             request.SpaceTypeId,
             request.Capacity,
             request.Description!,
             request.StandardRate,
             request.RequiresApproval,
             request.MaxReservationHours,
             request.IncomeAccountId,
             request.UpdatedBy
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteSpace(DeleteSpaceRequest request, CancellationToken cancellationToken)
    {
        var command = new DeleteSpaceCommand(
             request.Id,
             request.DeletedBy
         );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpGet]
    public async Task<IActionResult> GetSpaces(
       [FromQuery] BaseSpecParams specParams,
       CancellationToken cancellationToken
    )
    {
        var query = new GetSpacesQuery(specParams);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }


    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetSpace(
        long id,
        CancellationToken cancellationToken
     )
    {
        var query = new GetSpaceQuery(id);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }

}
