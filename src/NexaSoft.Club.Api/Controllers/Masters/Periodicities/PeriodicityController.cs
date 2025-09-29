using MediatR;
using Microsoft.AspNetCore.Mvc;
using NexaSoft.Club.Api.Controllers.Masters.Periodicities.Request;
using NexaSoft.Club.Application.Masters.Periodicities.Commands.CreatePeriodicity;
using NexaSoft.Club.Application.Masters.Periodicities.Commands.UpdatePeriodicity;
using NexaSoft.Club.Application.Masters.Periodicities.Commands.DeletePeriodicity;
using NexaSoft.Club.Application.Masters.Periodicities.Queries.GetPeriodicity;
using NexaSoft.Club.Application.Masters.Periodicities.Queries.GetPeriodicities;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Api.Extensions;

namespace NexaSoft.Club.Api.Controllers.Masters.Periodicities;

[Route("api/[controller]")]
[ApiController]
public class PeriodicityController(ISender _sender) : ControllerBase
{

    [HttpPost]
    public async Task<IActionResult> CreatePeriodicity(CreatePeriodicityRequest request, CancellationToken cancellationToken)
    {
        var command = new CreatePeriodicityCommand(
             request.Name,
             request.Description,
             request.CreatedBy
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpPut]
    public async Task<IActionResult> UpdatePeriodicity(UpdatePeriodicityRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdatePeriodicityCommand(
             request.Id,
             request.Name,
             request.Description,
             request.UpdatedBy
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpDelete]
    public async Task<IActionResult> DeletePeriodicity(DeletePeriodicityRequest request, CancellationToken cancellationToken)
    {
        var command = new DeletePeriodicityCommand(
             request.Id,
             request.DeletedBy
         );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpGet]
    public async Task<IActionResult> GetPeriodicities(
       [FromQuery] BaseSpecParams specParams,
       CancellationToken cancellationToken
    )
    {
        var query = new GetPeriodicitiesQuery(specParams);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }


    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetPeriodicity(
        long id,
        CancellationToken cancellationToken
     )
    {
        var query = new GetPeriodicityQuery(id);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }

}
