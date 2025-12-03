using MediatR;
using Microsoft.AspNetCore.Mvc;
using NexaSoft.Club.Api.Controllers.HumanResources.AttendanceRecords.Request;
using NexaSoft.Club.Application.HumanResources.AttendanceRecords.Commands.CreateAttendanceRecord;
using NexaSoft.Club.Application.HumanResources.AttendanceRecords.Commands.UpdateAttendanceRecord;
using NexaSoft.Club.Application.HumanResources.AttendanceRecords.Commands.DeleteAttendanceRecord;
using NexaSoft.Club.Application.HumanResources.AttendanceRecords.Queries.GetAttendanceRecord;
using NexaSoft.Club.Application.HumanResources.AttendanceRecords.Queries.GetAttendanceRecords;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Api.Extensions;
using NexaSoft.Club.Api.Attributes;
using Microsoft.AspNetCore.Authorization;

namespace NexaSoft.Club.Api.Controllers.HumanResources.AttendanceRecords;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class AttendanceRecordController(ISender _sender) : ControllerBase
{

    [HttpPost]
    [GeneratePermission]
    [RequirePermission("AttendanceRecord.CreateAttendanceRecord")]
    public async Task<IActionResult> CreateAttendanceRecord(CreateAttendanceRecordRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateAttendanceRecordCommand(
             request.EmployeeId,
             request.CostCenterId,
             request.RecordDate,
             request.CheckInTime,
             request.CheckOutTime,
             request.TotalHours,
             request.RegularHours,
             request.OvertimeHours,
             request.SundayHours,
             request.HolidayHours,
             request.NightHours,
             request.LateMinutes,
             request.EarlyDepartureMinutes,
             request.AttendanceStatusTypeId,
             request.CreatedBy
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpPut]
    [GeneratePermission]
    [RequirePermission("AttendanceRecord.UpdateAttendanceRecord")]
    public async Task<IActionResult> UpdateAttendanceRecord(UpdateAttendanceRecordRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateAttendanceRecordCommand(
             request.Id,
             request.EmployeeId,
             request.CostCenterId,
             request.RecordDate,
             request.CheckInTime,
             request.CheckOutTime,
             request.TotalHours,
             request.RegularHours,
             request.OvertimeHours,
             request.SundayHours,
             request.HolidayHours,
             request.NightHours,
             request.LateMinutes,
             request.EarlyDepartureMinutes,
             request.AttendanceStatusTypeId,
             request.UpdatedBy
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpDelete]
    [GeneratePermission]
    [RequirePermission("AttendanceRecord.DeleteAttendanceRecord")]
    public async Task<IActionResult> DeleteAttendanceRecord(DeleteAttendanceRecordRequest request, CancellationToken cancellationToken)
    {
        var command = new DeleteAttendanceRecordCommand(
             request.Id,
             request.DeletedBy
         );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpGet]
    [GeneratePermission]
    [RequirePermission("AttendanceRecord.GetAttendanceRecord")]
    public async Task<IActionResult> GetAttendanceRecords(
       [FromQuery] BaseSpecParams specParams,
       CancellationToken cancellationToken
    )
    {
        var query = new GetAttendanceRecordsQuery(specParams);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }


    [HttpGet("{id:long}")]
    [GeneratePermission]
    [RequirePermission("AttendanceRecord.GetAttendanceRecord")]
    public async Task<IActionResult> GetAttendanceRecord(
        long id,
        CancellationToken cancellationToken
     )
    {
        var query = new GetAttendanceRecordQuery(id);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }

}
