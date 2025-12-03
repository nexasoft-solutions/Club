using MediatR;
using Microsoft.AspNetCore.Mvc;
using NexaSoft.Club.Api.Controllers.HumanResources.PayrollConceptEmployees.Request;
using NexaSoft.Club.Application.HumanResources.PayrollConceptEmployees.Commands.CreatePayrollConceptEmployee;
using NexaSoft.Club.Application.HumanResources.PayrollConceptEmployees.Commands.UpdatePayrollConceptEmployee;
using NexaSoft.Club.Application.HumanResources.PayrollConceptEmployees.Commands.DeletePayrollConceptEmployee;
using NexaSoft.Club.Application.HumanResources.PayrollConceptEmployees.Queries.GetPayrollConceptEmployee;
using NexaSoft.Club.Application.HumanResources.PayrollConceptEmployees.Queries.GetPayrollConceptEmployees;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Api.Extensions;
using Microsoft.AspNetCore.Authorization;

namespace NexaSoft.Club.Api.Controllers.HumanResources.PayrollConceptEmployees;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class PayrollConceptEmployeeController(ISender _sender) : ControllerBase
{

    [HttpPost]
   public async Task<IActionResult> CreatePayrollConceptEmployee(CreatePayrollConceptEmployeeRequest request, CancellationToken cancellationToken)
    {
        var command = new CreatePayrollConceptEmployeeCommand(
             request.PayrollConceptId,
             request.EmployeeId,
    request.CreatedBy
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpPut]
   public async Task<IActionResult> UpdatePayrollConceptEmployee(UpdatePayrollConceptEmployeeRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdatePayrollConceptEmployeeCommand(
           request.Id,
             request.PayrollConceptId,
             request.EmployeeId,
             request.UpdatedBy
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpDelete]
   public async Task<IActionResult> DeletePayrollConceptEmployee(DeletePayrollConceptEmployeeRequest request, CancellationToken cancellationToken)
    {
        var command = new DeletePayrollConceptEmployeeCommand(
             request.Id,
             request.DeletedBy
         );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpGet]
    public async Task<IActionResult> GetPayrollConceptEmployees(
       [FromQuery] BaseSpecParams specParams,
       CancellationToken cancellationToken
    )
    {
        var query = new GetPayrollConceptEmployeesQuery(specParams);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }


   [HttpGet("{id:long}")]
   public async Task<IActionResult> GetPayrollConceptEmployee(
       long id,
       CancellationToken cancellationToken
    )
    {
        var query = new GetPayrollConceptEmployeeQuery(id);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }

}
