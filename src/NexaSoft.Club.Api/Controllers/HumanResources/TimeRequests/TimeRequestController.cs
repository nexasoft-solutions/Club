using MediatR;
using Microsoft.AspNetCore.Mvc;
using NexaSoft.Club.Api.Controllers.HumanResources.TimeRequests.Request;
using NexaSoft.Club.Application.HumanResources.TimeRequests.Commands.CreateTimeRequest;
using NexaSoft.Club.Application.HumanResources.TimeRequests.Commands.UpdateTimeRequest;
using NexaSoft.Club.Application.HumanResources.TimeRequests.Commands.DeleteTimeRequest;
using NexaSoft.Club.Application.HumanResources.TimeRequests.Queries.GetTimeRequest;
using NexaSoft.Club.Application.HumanResources.TimeRequests.Queries.GetTimeRequests;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Api.Extensions;

namespace NexaSoft.Club.Api.Controllers.HumanResources.TimeRequests;

[Route("api/[controller]")]
[ApiController]
public class TimeRequestController(ISender _sender) : ControllerBase
{

    [HttpPost]
   public async Task<IActionResult> CreateTimeRequest(CreateTimeRequestRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateTimeRequestCommand(
             request.EmployeeId,
             request.TimeRequestTypeId,
             request.StartDate,
             request.EndDate,
             request.TotalDays,
             request.Reason,
             request.StatusId,
    request.CreatedBy
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpPut]
   public async Task<IActionResult> UpdateTimeRequest(UpdateTimeRequestRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateTimeRequestCommand(
           request.Id,
             request.EmployeeId,
             request.TimeRequestTypeId,
             request.StartDate,
             request.EndDate,
             request.TotalDays,
             request.Reason,
             request.StatusId,
             request.UpdatedBy
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpDelete]
   public async Task<IActionResult> DeleteTimeRequest(DeleteTimeRequestRequest request, CancellationToken cancellationToken)
    {
        var command = new DeleteTimeRequestCommand(
             request.Id,
             request.DeletedBy
         );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpGet]
    public async Task<IActionResult> GetTimeRequests(
       [FromQuery] BaseSpecParams specParams,
       CancellationToken cancellationToken
    )
    {
        var query = new GetTimeRequestsQuery(specParams);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }


   [HttpGet("{id:long}")]
   public async Task<IActionResult> GetTimeRequest(
       long id,
       CancellationToken cancellationToken
    )
    {
        var query = new GetTimeRequestQuery(id);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }

}
