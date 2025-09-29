using MediatR;
using Microsoft.AspNetCore.Mvc;
using NexaSoft.Club.Api.Controllers.Masters.SpaceRates.Request;
using NexaSoft.Club.Application.Masters.SpaceRates.Commands.CreateSpaceRate;
using NexaSoft.Club.Application.Masters.SpaceRates.Commands.UpdateSpaceRate;
using NexaSoft.Club.Application.Masters.SpaceRates.Commands.DeleteSpaceRate;
using NexaSoft.Club.Application.Masters.SpaceRates.Queries.GetSpaceRate;
using NexaSoft.Club.Application.Masters.SpaceRates.Queries.GetSpaceRates;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Api.Extensions;

namespace NexaSoft.Club.Api.Controllers.Masters.SpaceRates;

[Route("api/[controller]")]
[ApiController]
public class SpaceRateController(ISender _sender) : ControllerBase
{

    [HttpPost]
   public async Task<IActionResult> CreateSpaceRate(CreateSpaceRateRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateSpaceRateCommand(
             request.SpaceId,
             request.MemberTypeId,
             request.Rate,
             request.IsActive,
    request.CreatedBy
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpPut]
   public async Task<IActionResult> UpdateSpaceRate(UpdateSpaceRateRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateSpaceRateCommand(
           request.Id,
             request.SpaceId,
             request.MemberTypeId,
             request.Rate,
             request.IsActive,
             request.UpdatedBy
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpDelete]
   public async Task<IActionResult> DeleteSpaceRate(DeleteSpaceRateRequest request, CancellationToken cancellationToken)
    {
        var command = new DeleteSpaceRateCommand(
             request.Id,
             request.DeletedBy
         );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpGet]
    public async Task<IActionResult> GetSpaceRates(
       [FromQuery] BaseSpecParams specParams,
       CancellationToken cancellationToken
    )
    {
        var query = new GetSpaceRatesQuery(specParams);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }


   [HttpGet("{id:long}")]
   public async Task<IActionResult> GetSpaceRate(
       long id,
       CancellationToken cancellationToken
    )
    {
        var query = new GetSpaceRateQuery(id);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }

}
