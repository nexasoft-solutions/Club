using MediatR;
using Microsoft.AspNetCore.Mvc;
using NexaSoft.Club.Api.Controllers.HumanResources.PayrollConfigs.Request;
using NexaSoft.Club.Application.HumanResources.PayrollConfigs.Commands.CreatePayrollConfig;
using NexaSoft.Club.Application.HumanResources.PayrollConfigs.Commands.UpdatePayrollConfig;
using NexaSoft.Club.Application.HumanResources.PayrollConfigs.Commands.DeletePayrollConfig;
using NexaSoft.Club.Application.HumanResources.PayrollConfigs.Queries.GetPayrollConfig;
using NexaSoft.Club.Application.HumanResources.PayrollConfigs.Queries.GetPayrollConfigs;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Api.Extensions;

namespace NexaSoft.Club.Api.Controllers.HumanResources.PayrollConfigs;

[Route("api/[controller]")]
[ApiController]
public class PayrollConfigController(ISender _sender) : ControllerBase
{

    [HttpPost]
   public async Task<IActionResult> CreatePayrollConfig(CreatePayrollConfigRequest request, CancellationToken cancellationToken)
    {
        var command = new CreatePayrollConfigCommand(
             request.CompanyId,
             request.PayPeriodTypeId,
             request.RegularHoursPerDay,
             request.WorkDaysPerWeek,
    request.CreatedBy
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpPut]
   public async Task<IActionResult> UpdatePayrollConfig(UpdatePayrollConfigRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdatePayrollConfigCommand(
           request.Id,
             request.CompanyId,
             request.PayPeriodTypeId,
             request.RegularHoursPerDay,
             request.WorkDaysPerWeek,
             request.UpdatedBy
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpDelete]
   public async Task<IActionResult> DeletePayrollConfig(DeletePayrollConfigRequest request, CancellationToken cancellationToken)
    {
        var command = new DeletePayrollConfigCommand(
             request.Id,
             request.DeletedBy
         );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpGet]
    public async Task<IActionResult> GetPayrollConfigs(
       [FromQuery] BaseSpecParams specParams,
       CancellationToken cancellationToken
    )
    {
        var query = new GetPayrollConfigsQuery(specParams);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }


   [HttpGet("{id:long}")]
   public async Task<IActionResult> GetPayrollConfig(
       long id,
       CancellationToken cancellationToken
    )
    {
        var query = new GetPayrollConfigQuery(id);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }

}
