using MediatR;
using Microsoft.AspNetCore.Mvc;
using NexaSoft.Club.Api.Controllers.HumanResources.PayrollPeriods.Request;
using NexaSoft.Club.Application.HumanResources.PayrollPeriods.Commands.CreatePayrollPeriod;
using NexaSoft.Club.Application.HumanResources.PayrollPeriods.Commands.UpdatePayrollPeriod;
using NexaSoft.Club.Application.HumanResources.PayrollPeriods.Commands.DeletePayrollPeriod;
using NexaSoft.Club.Application.HumanResources.PayrollPeriods.Queries.GetPayrollPeriod;
using NexaSoft.Club.Application.HumanResources.PayrollPeriods.Queries.GetPayrollPeriods;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Api.Extensions;

namespace NexaSoft.Club.Api.Controllers.HumanResources.PayrollPeriods;

[Route("api/[controller]")]
[ApiController]
public class PayrollPeriodController(ISender _sender) : ControllerBase
{

    [HttpPost]
    public async Task<IActionResult> CreatePayrollPeriod(CreatePayrollPeriodRequest request, CancellationToken cancellationToken)
    {
        var command = new CreatePayrollPeriodCommand(
             request.PeriodName,
             request.PayrollTypeId,
             request.StartDate,
             request.EndDate,
             request.CreatedBy
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpPut]
    public async Task<IActionResult> UpdatePayrollPeriod(UpdatePayrollPeriodRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdatePayrollPeriodCommand(
             request.Id,
             request.PeriodName,
             request.PayrollTypeId,
             request.StartDate,
             request.EndDate,
             request.TotalAmount,
             request.TotalEmployees,
             request.StatusId,
             request.UpdatedBy
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpDelete]
    public async Task<IActionResult> DeletePayrollPeriod(DeletePayrollPeriodRequest request, CancellationToken cancellationToken)
    {
        var command = new DeletePayrollPeriodCommand(
             request.Id,
             request.DeletedBy
         );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpGet]
    public async Task<IActionResult> GetPayrollPeriods(
       [FromQuery] BaseSpecParams specParams,
       CancellationToken cancellationToken
    )
    {
        var query = new GetPayrollPeriodsQuery(specParams);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }


    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetPayrollPeriod(
        long id,
        CancellationToken cancellationToken
     )
    {
        var query = new GetPayrollPeriodQuery(id);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }

}
