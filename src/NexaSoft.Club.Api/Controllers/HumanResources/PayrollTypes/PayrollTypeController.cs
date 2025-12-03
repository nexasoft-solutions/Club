using MediatR;
using Microsoft.AspNetCore.Mvc;
using NexaSoft.Club.Api.Controllers.HumanResources.PayrollTypes.Request;
using NexaSoft.Club.Application.HumanResources.PayrollTypes.Commands.CreatePayrollType;
using NexaSoft.Club.Application.HumanResources.PayrollTypes.Commands.UpdatePayrollType;
using NexaSoft.Club.Application.HumanResources.PayrollTypes.Commands.DeletePayrollType;
using NexaSoft.Club.Application.HumanResources.PayrollTypes.Queries.GetPayrollType;
using NexaSoft.Club.Application.HumanResources.PayrollTypes.Queries.GetPayrollTypes;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Api.Extensions;
using Microsoft.AspNetCore.Authorization;
using NexaSoft.Club.Api.Attributes;

namespace NexaSoft.Club.Api.Controllers.HumanResources.PayrollTypes;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class PayrollTypeController(ISender _sender) : ControllerBase
{

    [HttpPost]
    [GeneratePermission]
    [RequirePermission("PayrollType.CreatePayrollType")]
    public async Task<IActionResult> CreatePayrollType(CreatePayrollTypeRequest request, CancellationToken cancellationToken)
    {
        var command = new CreatePayrollTypeCommand(
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
    [RequirePermission("PayrollType.UpdatePayrollType")]
    public async Task<IActionResult> UpdatePayrollType(UpdatePayrollTypeRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdatePayrollTypeCommand(
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
    [RequirePermission("PayrollType.DeletePayrollType")]
    public async Task<IActionResult> DeletePayrollType(DeletePayrollTypeRequest request, CancellationToken cancellationToken)
    {
        var command = new DeletePayrollTypeCommand(
             request.Id,
             request.DeletedBy
         );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpGet]   
    [GeneratePermission]
    [RequirePermission("PayrollType.GetPayrollType")]
    public async Task<IActionResult> GetPayrollTypes(
       [FromQuery] BaseSpecParams specParams,
       CancellationToken cancellationToken
    )
    {
        var query = new GetPayrollTypesQuery(specParams);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }


    [HttpGet("{id:long}")]
    [GeneratePermission]
    [RequirePermission("PayrollType.GetPayrollType")]
    public async Task<IActionResult> GetPayrollType(
        long id,
        CancellationToken cancellationToken
     )
    {
        var query = new GetPayrollTypeQuery(id);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }

}
