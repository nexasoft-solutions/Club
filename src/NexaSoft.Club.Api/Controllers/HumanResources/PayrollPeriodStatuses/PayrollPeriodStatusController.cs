using MediatR;
using Microsoft.AspNetCore.Mvc;
using NexaSoft.Club.Api.Controllers.HumanResources.PayrollPeriodStatuses.Request;
using NexaSoft.Club.Application.HumanResources.PayrollPeriodStatuses.Commands.CreatePayrollPeriodStatus;
using NexaSoft.Club.Application.HumanResources.PayrollPeriodStatuses.Commands.UpdatePayrollPeriodStatus;
using NexaSoft.Club.Application.HumanResources.PayrollPeriodStatuses.Commands.DeletePayrollPeriodStatus;
using NexaSoft.Club.Application.HumanResources.PayrollPeriodStatuses.Queries.GetPayrollPeriodStatus;
using NexaSoft.Club.Application.HumanResources.PayrollPeriodStatuses.Queries.GetPayrollPeriodStatuses;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Api.Extensions;
using Microsoft.AspNetCore.Authorization;
using NexaSoft.Club.Api.Attributes;

namespace NexaSoft.Club.Api.Controllers.HumanResources.PayrollPeriodStatuses;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class PayrollPeriodStatusController(ISender _sender) : ControllerBase
{

    [HttpPost] 
    [GeneratePermission]
    [RequirePermission("PayrollPeriodStatus.CreatePayrollPeriodStatus")]
    public async Task<IActionResult> CreatePayrollPeriodStatus(CreatePayrollPeriodStatusRequest request, CancellationToken cancellationToken)
    {
        var command = new CreatePayrollPeriodStatusCommand(
             request.Code,
             request.Name,
             request.Description,
             request.CreatedBy
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpPut]
    [GeneratePermission]
    [RequirePermission("PayrollPeriodStatus.UpdatePayrollPeriodStatus")]
    public async Task<IActionResult> UpdatePayrollPeriodStatus(UpdatePayrollPeriodStatusRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdatePayrollPeriodStatusCommand(
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
    [GeneratePermission]
    [RequirePermission("PayrollPeriodStatus.DeletePayrollPeriodStatus")]
    public async Task<IActionResult> DeletePayrollPeriodStatus(DeletePayrollPeriodStatusRequest request, CancellationToken cancellationToken)
    {
        var command = new DeletePayrollPeriodStatusCommand(
             request.Id,
             request.DeletedBy
         );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpGet]
    [GeneratePermission]
    [RequirePermission("PayrollPeriodStatus.GetPayrollPeriodStatus")]
    public async Task<IActionResult> GetPayrollPeriodStatuses(
       [FromQuery] BaseSpecParams specParams,
       CancellationToken cancellationToken
    )
    {
        var query = new GetPayrollPeriodStatusesQuery(specParams);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }


    [HttpGet("{id:long}")]
    [GeneratePermission]
    [RequirePermission("PayrollPeriodStatus.GetPayrollPeriodStatus")]
    public async Task<IActionResult> GetPayrollPeriodStatus(
        long id,
        CancellationToken cancellationToken
     )
    {
        var query = new GetPayrollPeriodStatusQuery(id);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }

}
