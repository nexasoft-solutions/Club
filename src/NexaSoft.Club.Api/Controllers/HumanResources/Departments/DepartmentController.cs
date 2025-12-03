using MediatR;
using Microsoft.AspNetCore.Mvc;
using NexaSoft.Club.Api.Controllers.HumanResources.Departments.Request;
using NexaSoft.Club.Application.HumanResources.Departments.Commands.CreateDepartment;
using NexaSoft.Club.Application.HumanResources.Departments.Commands.UpdateDepartment;
using NexaSoft.Club.Application.HumanResources.Departments.Commands.DeleteDepartment;
using NexaSoft.Club.Application.HumanResources.Departments.Queries.GetDepartment;
using NexaSoft.Club.Application.HumanResources.Departments.Queries.GetDepartments;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Api.Extensions;
using Microsoft.AspNetCore.Authorization;
using NexaSoft.Club.Api.Attributes;

namespace NexaSoft.Club.Api.Controllers.HumanResources.Departments;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class DepartmentController(ISender _sender) : ControllerBase
{

    [HttpPost]
    [GeneratePermission]
    [RequirePermission("Department.CreateDepartment")]
    public async Task<IActionResult> CreateDepartment(CreateDepartmentRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateDepartmentCommand(
             request.Code,
             request.Name,
             request.ParentDepartmentId,
             request.Description,
             request.ManagerId,
             request.CostCenterId,
             request.Location,
             request.PhoneExtension,
             request.CreatedBy
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpPut]
    [GeneratePermission]
    [RequirePermission("Department.UpdateDepartment")]
    public async Task<IActionResult> UpdateDepartment(UpdateDepartmentRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateDepartmentCommand(
             request.Id,
             request.Code,
             request.Name,
             request.ParentDepartmentId,
             request.Description,
             request.ManagerId,
             request.CostCenterId,
             request.Location,
             request.PhoneExtension,
             request.UpdatedBy
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpDelete]
    [GeneratePermission]
    [RequirePermission("Department.DeleteDepartment")]
    public async Task<IActionResult> DeleteDepartment(DeleteDepartmentRequest request, CancellationToken cancellationToken)
    {
        var command = new DeleteDepartmentCommand(
             request.Id,
             request.DeletedBy
         );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpGet]
    [GeneratePermission]
    [RequirePermission("Department.GetDepartment")]
    public async Task<IActionResult> GetDepartments(
       [FromQuery] BaseSpecParams specParams,
       CancellationToken cancellationToken
    )
    {
        var query = new GetDepartmentsQuery(specParams);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }


    [HttpGet("{id:long}")]
    [GeneratePermission]
    [RequirePermission("Department.GetDepartment")]
    public async Task<IActionResult> GetDepartment(
        long id,
        CancellationToken cancellationToken
     )
    {
        var query = new GetDepartmentQuery(id);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }

}
