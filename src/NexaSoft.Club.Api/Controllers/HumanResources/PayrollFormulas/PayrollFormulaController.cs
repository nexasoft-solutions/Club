using MediatR;
using Microsoft.AspNetCore.Mvc;
using NexaSoft.Club.Api.Controllers.HumanResources.PayrollFormulas.Request;
using NexaSoft.Club.Application.HumanResources.PayrollFormulas.Commands.CreatePayrollFormula;
using NexaSoft.Club.Application.HumanResources.PayrollFormulas.Commands.UpdatePayrollFormula;
using NexaSoft.Club.Application.HumanResources.PayrollFormulas.Commands.DeletePayrollFormula;
using NexaSoft.Club.Application.HumanResources.PayrollFormulas.Queries.GetPayrollFormula;
using NexaSoft.Club.Application.HumanResources.PayrollFormulas.Queries.GetPayrollFormulas;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Api.Extensions;

namespace NexaSoft.Club.Api.Controllers.HumanResources.PayrollFormulas;

[Route("api/[controller]")]
[ApiController]
public class PayrollFormulaController(ISender _sender) : ControllerBase
{

    [HttpPost]
    public async Task<IActionResult> CreatePayrollFormula(CreatePayrollFormulaRequest request, CancellationToken cancellationToken)
    {
        var command = new CreatePayrollFormulaCommand(
             request.Code,
             request.Name,
             request.FormulaExpression,
             request.Description,
             request.RequiredVariables,
             request.Variables,
             request.CreatedBy
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpPut]
    public async Task<IActionResult> UpdatePayrollFormula(UpdatePayrollFormulaRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdatePayrollFormulaCommand(
             request.Id,
             request.Code,
             request.Name,
             request.FormulaExpression,
             request.Description,
             request.RequiredVariables,
             request.Variables,
             request.UpdatedBy
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpDelete]
    public async Task<IActionResult> DeletePayrollFormula(DeletePayrollFormulaRequest request, CancellationToken cancellationToken)
    {
        var command = new DeletePayrollFormulaCommand(
             request.Id,
             request.DeletedBy
         );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpGet]
    public async Task<IActionResult> GetPayrollFormulas(
       [FromQuery] BaseSpecParams specParams,
       CancellationToken cancellationToken
    )
    {
        var query = new GetPayrollFormulasQuery(specParams);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }


    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetPayrollFormula(
        long id,
        CancellationToken cancellationToken
     )
    {
        var query = new GetPayrollFormulaQuery(id);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }

}
