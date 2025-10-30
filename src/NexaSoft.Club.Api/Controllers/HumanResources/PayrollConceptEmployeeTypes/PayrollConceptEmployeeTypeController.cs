using MediatR;
using Microsoft.AspNetCore.Mvc;
using NexaSoft.Club.Api.Controllers.HumanResources.PayrollConceptEmployeeTypes.Request;
using NexaSoft.Club.Application.HumanResources.PayrollConceptEmployeeTypes.Commands.CreatePayrollConceptEmployeeType;
using NexaSoft.Club.Application.HumanResources.PayrollConceptEmployeeTypes.Commands.UpdatePayrollConceptEmployeeType;
using NexaSoft.Club.Application.HumanResources.PayrollConceptEmployeeTypes.Commands.DeletePayrollConceptEmployeeType;
using NexaSoft.Club.Application.HumanResources.PayrollConceptEmployeeTypes.Queries.GetPayrollConceptEmployeeType;
using NexaSoft.Club.Application.HumanResources.PayrollConceptEmployeeTypes.Queries.GetPayrollConceptEmployeeTypes;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Api.Extensions;

namespace NexaSoft.Club.Api.Controllers.HumanResources.PayrollConceptEmployeeTypes;

[Route("api/[controller]")]
[ApiController]
public class PayrollConceptEmployeeTypeController(ISender _sender) : ControllerBase
{

    [HttpPost]
   public async Task<IActionResult> CreatePayrollConceptEmployeeType(CreatePayrollConceptEmployeeTypeRequest request, CancellationToken cancellationToken)
    {
        var command = new CreatePayrollConceptEmployeeTypeCommand(
             request.PayrollConceptId,
             request.EmployeeTypeId,
    request.CreatedBy
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpPut]
   public async Task<IActionResult> UpdatePayrollConceptEmployeeType(UpdatePayrollConceptEmployeeTypeRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdatePayrollConceptEmployeeTypeCommand(
           request.Id,
             request.PayrollConceptId,
             request.EmployeeTypeId,
             request.UpdatedBy
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpDelete]
   public async Task<IActionResult> DeletePayrollConceptEmployeeType(DeletePayrollConceptEmployeeTypeRequest request, CancellationToken cancellationToken)
    {
        var command = new DeletePayrollConceptEmployeeTypeCommand(
             request.Id,
             request.DeletedBy
         );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpGet]
    public async Task<IActionResult> GetPayrollConceptEmployeeTypes(
       [FromQuery] BaseSpecParams specParams,
       CancellationToken cancellationToken
    )
    {
        var query = new GetPayrollConceptEmployeeTypesQuery(specParams);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }


   [HttpGet("{id:long}")]
   public async Task<IActionResult> GetPayrollConceptEmployeeType(
       long id,
       CancellationToken cancellationToken
    )
    {
        var query = new GetPayrollConceptEmployeeTypeQuery(id);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }

}
