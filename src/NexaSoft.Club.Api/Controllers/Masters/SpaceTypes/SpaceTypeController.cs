using MediatR;
using Microsoft.AspNetCore.Mvc;
using NexaSoft.Club.Api.Controllers.Masters.SpaceTypes.Request;
using NexaSoft.Club.Application.Masters.SpaceTypes.Commands.CreateSpaceType;
using NexaSoft.Club.Application.Masters.SpaceTypes.Commands.UpdateSpaceType;
using NexaSoft.Club.Application.Masters.SpaceTypes.Commands.DeleteSpaceType;
using NexaSoft.Club.Application.Masters.SpaceTypes.Queries.GetSpaceType;
using NexaSoft.Club.Application.Masters.SpaceTypes.Queries.GetSpaceTypes;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Api.Extensions;
using Microsoft.AspNetCore.Authorization;
using NexaSoft.Club.Api.Attributes;

namespace NexaSoft.Club.Api.Controllers.Masters.SpaceTypes;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class SpaceTypeController(ISender _sender) : ControllerBase
{

    [HttpPost]
    [GeneratePermission]
    [RequirePermission("SpaceType.CreateSpaceType")]
    public async Task<IActionResult> CreateSpaceType(CreateSpaceTypeRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateSpaceTypeCommand(
             request.Name,
             request.Description,
             request.CreatedBy
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpPut]
    [GeneratePermission]
    [RequirePermission("SpaceType.UpdateSpaceType")]
    public async Task<IActionResult> UpdateSpaceType(UpdateSpaceTypeRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateSpaceTypeCommand(
             request.Id,
             request.Name,
             request.Description,
             request.UpdatedBy
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpDelete]
    [GeneratePermission]
    [RequirePermission("SpaceType.DeleteSpaceType")]
    public async Task<IActionResult> DeleteSpaceType(DeleteSpaceTypeRequest request, CancellationToken cancellationToken)
    {
        var command = new DeleteSpaceTypeCommand(
             request.Id,
             request.DeletedBy
         );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpGet]
    [GeneratePermission]
    [RequirePermission("SpaceType.GetSpaceTypes")]
    public async Task<IActionResult> GetSpaceTypes(
       [FromQuery] BaseSpecParams specParams,
       CancellationToken cancellationToken
    )
    {
        var query = new GetSpaceTypesQuery(specParams);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }


    [HttpGet("{id:long}")]
    [GeneratePermission]
    [RequirePermission("SpaceType.GetSpaceType")]
    public async Task<IActionResult> GetSpaceType(
        long id,
        CancellationToken cancellationToken
     )
    {
        var query = new GetSpaceTypeQuery(id);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }

}
