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

namespace NexaSoft.Club.Api.Controllers.HumanResources.PayrollPeriodStatuses;

[Route("api/[controller]")]
[ApiController]
public class PayrollPeriodStatusController(ISender _sender) : ControllerBase
{

    [HttpPost]
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
