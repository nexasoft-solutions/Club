using MediatR;
using Microsoft.AspNetCore.Mvc;
using NexaSoft.Club.Api.Controllers.HumanResources.TimeRequestTypes.Request;
using NexaSoft.Club.Application.HumanResources.TimeRequestTypes.Commands.CreateTimeRequestType;
using NexaSoft.Club.Application.HumanResources.TimeRequestTypes.Commands.UpdateTimeRequestType;
using NexaSoft.Club.Application.HumanResources.TimeRequestTypes.Commands.DeleteTimeRequestType;
using NexaSoft.Club.Application.HumanResources.TimeRequestTypes.Queries.GetTimeRequestType;
using NexaSoft.Club.Application.HumanResources.TimeRequestTypes.Queries.GetTimeRequestTypes;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Api.Extensions;

namespace NexaSoft.Club.Api.Controllers.HumanResources.TimeRequestTypes;

[Route("api/[controller]")]
[ApiController]
public class TimeRequestTypeController(ISender _sender) : ControllerBase
{

    [HttpPost]
   public async Task<IActionResult> CreateTimeRequestType(CreateTimeRequestTypeRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateTimeRequestTypeCommand(
             request.Code,
             request.Name,
             request.Description,
    request.CreatedBy
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpPut]
   public async Task<IActionResult> UpdateTimeRequestType(UpdateTimeRequestTypeRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateTimeRequestTypeCommand(
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
   public async Task<IActionResult> DeleteTimeRequestType(DeleteTimeRequestTypeRequest request, CancellationToken cancellationToken)
    {
        var command = new DeleteTimeRequestTypeCommand(
             request.Id,
             request.DeletedBy
         );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpGet]
    public async Task<IActionResult> GetTimeRequestTypes(
       [FromQuery] BaseSpecParams specParams,
       CancellationToken cancellationToken
    )
    {
        var query = new GetTimeRequestTypesQuery(specParams);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }


   [HttpGet("{id:long}")]
   public async Task<IActionResult> GetTimeRequestType(
       long id,
       CancellationToken cancellationToken
    )
    {
        var query = new GetTimeRequestTypeQuery(id);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }

}
