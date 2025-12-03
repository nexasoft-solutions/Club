using MediatR;
using Microsoft.AspNetCore.Mvc;
using NexaSoft.Club.Api.Controllers.HumanResources.PayrollConceptDepartments.Request;
using NexaSoft.Club.Application.HumanResources.PayrollConceptDepartments.Commands.CreatePayrollConceptDepartment;
using NexaSoft.Club.Application.HumanResources.PayrollConceptDepartments.Commands.UpdatePayrollConceptDepartment;
using NexaSoft.Club.Application.HumanResources.PayrollConceptDepartments.Commands.DeletePayrollConceptDepartment;
using NexaSoft.Club.Application.HumanResources.PayrollConceptDepartments.Queries.GetPayrollConceptDepartment;
using NexaSoft.Club.Application.HumanResources.PayrollConceptDepartments.Queries.GetPayrollConceptDepartments;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Api.Extensions;
using Microsoft.AspNetCore.Authorization;
using NexaSoft.Club.Api.Attributes;

namespace NexaSoft.Club.Api.Controllers.HumanResources.PayrollConceptDepartments;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class PayrollConceptDepartmentController(ISender _sender) : ControllerBase
{

    [HttpPost]
    [GeneratePermission]
    [RequirePermission("PayrollConceptDepartment.CreatePayrollConceptDepartment")]
    public async Task<IActionResult> CreatePayrollConceptDepartment(CreatePayrollConceptDepartmentRequest request, CancellationToken cancellationToken)
    {
        var command = new CreatePayrollConceptDepartmentCommand(
             request.PayrollConceptId,
             request.DepartmentId,
             request.CreatedBy
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpPut]
    [GeneratePermission]
    [RequirePermission("PayrollConceptDepartment.UpdatePayrollConceptDepartment")]
    public async Task<IActionResult> UpdatePayrollConceptDepartment(UpdatePayrollConceptDepartmentRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdatePayrollConceptDepartmentCommand(
             request.Id,
             request.PayrollConceptId,
             request.DepartmentId,
             request.UpdatedBy
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpDelete]
    [GeneratePermission]
    [RequirePermission("PayrollConceptDepartment.DeletePayrollConceptDepartment")]
    public async Task<IActionResult> DeletePayrollConceptDepartment(DeletePayrollConceptDepartmentRequest request, CancellationToken cancellationToken)
    {
        var command = new DeletePayrollConceptDepartmentCommand(
             request.Id,
             request.DeletedBy
         );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpGet]
    [GeneratePermission]
    [RequirePermission("PayrollConceptDepartment.GetPayrollConceptDepartment")]
    public async Task<IActionResult> GetPayrollConceptDepartments(
       [FromQuery] BaseSpecParams specParams,
       CancellationToken cancellationToken
    )
    {
        var query = new GetPayrollConceptDepartmentsQuery(specParams);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }


    [HttpGet("{id:long}")]
    [GeneratePermission]
    [RequirePermission("PayrollConceptDepartment.GetPayrollConceptDepartment")]
    public async Task<IActionResult> GetPayrollConceptDepartment(
        long id,
        CancellationToken cancellationToken
     )
    {
        var query = new GetPayrollConceptDepartmentQuery(id);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }

}
