using MediatR;
using Microsoft.AspNetCore.Mvc;
using NexaSoft.Club.Api.Controllers.HumanResources.EmployeeTypes.Request;
using NexaSoft.Club.Application.HumanResources.EmployeeTypes.Commands.CreateEmployeeType;
using NexaSoft.Club.Application.HumanResources.EmployeeTypes.Commands.UpdateEmployeeType;
using NexaSoft.Club.Application.HumanResources.EmployeeTypes.Commands.DeleteEmployeeType;
using NexaSoft.Club.Application.HumanResources.EmployeeTypes.Queries.GetEmployeeType;
using NexaSoft.Club.Application.HumanResources.EmployeeTypes.Queries.GetEmployeeTypes;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Api.Extensions;
using Microsoft.AspNetCore.Authorization;
using NexaSoft.Club.Api.Attributes;

namespace NexaSoft.Club.Api.Controllers.HumanResources.EmployeeTypes;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class EmployeeTypeController(ISender _sender) : ControllerBase
{

    [HttpPost] 
    [GeneratePermission]
    [RequirePermission("EmployeeType.CreateEmployeeType")]
    public async Task<IActionResult> CreateEmployeeType(CreateEmployeeTypeRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateEmployeeTypeCommand(
             request.Code,
             request.Name,
             request.Description,
             request.BaseSalary,
             request.CreatedBy
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpPut]
    [GeneratePermission]
    [RequirePermission("EmployeeType.UpdateEmployeeType")]
    public async Task<IActionResult> UpdateEmployeeType(UpdateEmployeeTypeRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateEmployeeTypeCommand(
             request.Id,
             request.Code,
             request.Name,
             request.Description,
             request.BaseSalary,
             request.UpdatedBy
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpDelete]
    [GeneratePermission]
    [RequirePermission("EmployeeType.DeleteEmployeeType")]
    public async Task<IActionResult> DeleteEmployeeType(DeleteEmployeeTypeRequest request, CancellationToken cancellationToken)
    {
        var command = new DeleteEmployeeTypeCommand(
             request.Id,
             request.DeletedBy
         );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpGet]
    [GeneratePermission]
    [RequirePermission("EmployeeType.GetEmployeeType")]
    public async Task<IActionResult> GetEmployeeTypes(
       [FromQuery] BaseSpecParams specParams,
       CancellationToken cancellationToken
    )
    {
        var query = new GetEmployeeTypesQuery(specParams);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }


    [HttpGet("{id:long}")]
    [GeneratePermission]
    [RequirePermission("EmployeeType.GetEmployeeType")]
    public async Task<IActionResult> GetEmployeeType(
        long id,
        CancellationToken cancellationToken
     )
    {
        var query = new GetEmployeeTypeQuery(id);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }

}
