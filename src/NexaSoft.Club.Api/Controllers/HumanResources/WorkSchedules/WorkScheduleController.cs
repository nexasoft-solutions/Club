using MediatR;
using Microsoft.AspNetCore.Mvc;
using NexaSoft.Club.Api.Controllers.HumanResources.WorkSchedules.Request;
using NexaSoft.Club.Application.HumanResources.WorkSchedules.Commands.CreateWorkSchedule;
using NexaSoft.Club.Application.HumanResources.WorkSchedules.Commands.UpdateWorkSchedule;
using NexaSoft.Club.Application.HumanResources.WorkSchedules.Commands.DeleteWorkSchedule;
using NexaSoft.Club.Application.HumanResources.WorkSchedules.Queries.GetWorkSchedule;
using NexaSoft.Club.Application.HumanResources.WorkSchedules.Queries.GetWorkSchedules;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Api.Extensions;

namespace NexaSoft.Club.Api.Controllers.HumanResources.WorkSchedules;

[Route("api/[controller]")]
[ApiController]
public class WorkScheduleController(ISender _sender) : ControllerBase
{

    [HttpPost]
   public async Task<IActionResult> CreateWorkSchedule(CreateWorkScheduleRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateWorkScheduleCommand(
             request.EmployeeId,
             request.DayOfWeek,
             request.StartTime,
             request.EndTime,
    request.CreatedBy
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpPut]
   public async Task<IActionResult> UpdateWorkSchedule(UpdateWorkScheduleRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateWorkScheduleCommand(
           request.Id,
             request.EmployeeId,
             request.DayOfWeek,
             request.StartTime,
             request.EndTime,
             request.UpdatedBy
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpDelete]
   public async Task<IActionResult> DeleteWorkSchedule(DeleteWorkScheduleRequest request, CancellationToken cancellationToken)
    {
        var command = new DeleteWorkScheduleCommand(
             request.Id,
             request.DeletedBy
         );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpGet]
    public async Task<IActionResult> GetWorkSchedules(
       [FromQuery] BaseSpecParams specParams,
       CancellationToken cancellationToken
    )
    {
        var query = new GetWorkSchedulesQuery(specParams);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }


   [HttpGet("{id:long}")]
   public async Task<IActionResult> GetWorkSchedule(
       long id,
       CancellationToken cancellationToken
    )
    {
        var query = new GetWorkScheduleQuery(id);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }

}
