using MediatR;
using Microsoft.AspNetCore.Mvc;
using NexaSoft.Club.Api.Controllers.HumanResources.RateTypes.Request;
using NexaSoft.Club.Application.HumanResources.RateTypes.Commands.CreateRateType;
using NexaSoft.Club.Application.HumanResources.RateTypes.Commands.UpdateRateType;
using NexaSoft.Club.Application.HumanResources.RateTypes.Commands.DeleteRateType;
using NexaSoft.Club.Application.HumanResources.RateTypes.Queries.GetRateType;
using NexaSoft.Club.Application.HumanResources.RateTypes.Queries.GetRateTypes;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Api.Extensions;

namespace NexaSoft.Club.Api.Controllers.HumanResources.RateTypes;

[Route("api/[controller]")]
[ApiController]
public class RateTypeController(ISender _sender) : ControllerBase
{

    [HttpPost]
   public async Task<IActionResult> CreateRateType(CreateRateTypeRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateRateTypeCommand(
             request.Code,
             request.Name,
             request.Description,
    request.CreatedBy
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpPut]
   public async Task<IActionResult> UpdateRateType(UpdateRateTypeRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateRateTypeCommand(
           request.Id,
             request.Code,
             request.Name,
             request.Description,
             request.UpdatedBy
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpDelete]
   public async Task<IActionResult> DeleteRateType(DeleteRateTypeRequest request, CancellationToken cancellationToken)
    {
        var command = new DeleteRateTypeCommand(
             request.Id,
             request.DeletedBy
         );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpGet]
    public async Task<IActionResult> GetRateTypes(
       [FromQuery] BaseSpecParams specParams,
       CancellationToken cancellationToken
    )
    {
        var query = new GetRateTypesQuery(specParams);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }


   [HttpGet("{id:long}")]
   public async Task<IActionResult> GetRateType(
       long id,
       CancellationToken cancellationToken
    )
    {
        var query = new GetRateTypeQuery(id);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }

}
