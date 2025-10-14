using MediatR;
using Microsoft.AspNetCore.Mvc;
using NexaSoft.Club.Api.Controllers.Masters.Statuses.Request;
using NexaSoft.Club.Application.Masters.Statuses.Commands.CreateStatus;
using NexaSoft.Club.Application.Masters.Statuses.Commands.UpdateStatus;
using NexaSoft.Club.Application.Masters.Statuses.Commands.DeleteStatus;
using NexaSoft.Club.Application.Masters.Statuses.Queries.GetStatus;
using NexaSoft.Club.Application.Masters.Statuses.Queries.GetStatuses;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Api.Extensions;

namespace NexaSoft.Club.Api.Controllers.Masters.Statuses;

[Route("api/[controller]")]
[ApiController]
public class StatusController(ISender _sender) : ControllerBase
{

    [HttpPost]
   public async Task<IActionResult> CreateStatus(CreateStatusRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateStatusCommand(
             request.Name,
             request.Description,
    request.CreatedBy
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpPut]
   public async Task<IActionResult> UpdateStatus(UpdateStatusRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateStatusCommand(
           request.Id,
             request.Name,
             request.Description,
             request.UpdatedBy
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpDelete]
   public async Task<IActionResult> DeleteStatus(DeleteStatusRequest request, CancellationToken cancellationToken)
    {
        var command = new DeleteStatusCommand(
             request.Id,
             request.DeletedBy
         );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpGet]
    public async Task<IActionResult> GetStatuses(
       [FromQuery] BaseSpecParams specParams,
       CancellationToken cancellationToken
    )
    {
        var query = new GetStatusesQuery(specParams);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }


   [HttpGet("{id:long}")]
   public async Task<IActionResult> GetStatus(
       long id,
       CancellationToken cancellationToken
    )
    {
        var query = new GetStatusQuery(id);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }

}
