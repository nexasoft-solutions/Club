using MediatR;
using Microsoft.AspNetCore.Mvc;
using NexaSoft.Club.Api.Controllers.HumanResources.AttendanceStatusTypes.Request;
using NexaSoft.Club.Application.HumanResources.AttendanceStatusTypes.Commands.CreateAttendanceStatusType;
using NexaSoft.Club.Application.HumanResources.AttendanceStatusTypes.Commands.UpdateAttendanceStatusType;
using NexaSoft.Club.Application.HumanResources.AttendanceStatusTypes.Commands.DeleteAttendanceStatusType;
using NexaSoft.Club.Application.HumanResources.AttendanceStatusTypes.Queries.GetAttendanceStatusType;
using NexaSoft.Club.Application.HumanResources.AttendanceStatusTypes.Queries.GetAttendanceStatusTypes;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Api.Extensions;

namespace NexaSoft.Club.Api.Controllers.HumanResources.AttendanceStatusTypes;

[Route("api/[controller]")]
[ApiController]
public class AttendanceStatusTypeController(ISender _sender) : ControllerBase
{

    [HttpPost]
   public async Task<IActionResult> CreateAttendanceStatusType(CreateAttendanceStatusTypeRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateAttendanceStatusTypeCommand(
             request.Code,
             request.Name,
             request.IsPaid,
             request.Description,
    request.CreatedBy
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpPut]
   public async Task<IActionResult> UpdateAttendanceStatusType(UpdateAttendanceStatusTypeRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateAttendanceStatusTypeCommand(
           request.Id,
             request.Code,
             request.Name,
             request.IsPaid,
             request.Description,
             request.UpdatedBy
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpDelete]
   public async Task<IActionResult> DeleteAttendanceStatusType(DeleteAttendanceStatusTypeRequest request, CancellationToken cancellationToken)
    {
        var command = new DeleteAttendanceStatusTypeCommand(
             request.Id,
             request.DeletedBy
         );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpGet]
    public async Task<IActionResult> GetAttendanceStatusTypes(
       [FromQuery] BaseSpecParams specParams,
       CancellationToken cancellationToken
    )
    {
        var query = new GetAttendanceStatusTypesQuery(specParams);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }


   [HttpGet("{id:long}")]
   public async Task<IActionResult> GetAttendanceStatusType(
       long id,
       CancellationToken cancellationToken
    )
    {
        var query = new GetAttendanceStatusTypeQuery(id);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }

}
