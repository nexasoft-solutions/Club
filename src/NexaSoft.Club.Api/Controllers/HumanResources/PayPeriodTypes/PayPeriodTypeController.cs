using MediatR;
using Microsoft.AspNetCore.Mvc;
using NexaSoft.Club.Api.Controllers.HumanResources.PayPeriodTypes.Request;
using NexaSoft.Club.Application.HumanResources.PayPeriodTypes.Commands.CreatePayPeriodType;
using NexaSoft.Club.Application.HumanResources.PayPeriodTypes.Commands.UpdatePayPeriodType;
using NexaSoft.Club.Application.HumanResources.PayPeriodTypes.Commands.DeletePayPeriodType;
using NexaSoft.Club.Application.HumanResources.PayPeriodTypes.Queries.GetPayPeriodType;
using NexaSoft.Club.Application.HumanResources.PayPeriodTypes.Queries.GetPayPeriodTypes;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Api.Extensions;
using Microsoft.AspNetCore.Authorization;
using NexaSoft.Club.Api.Attributes;

namespace NexaSoft.Club.Api.Controllers.HumanResources.PayPeriodTypes;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class PayPeriodTypeController(ISender _sender) : ControllerBase
{

    [HttpPost]
    [GeneratePermission]
    [RequirePermission("PayPeriodType.CreatePayPeriodType")]
    public async Task<IActionResult> CreatePayPeriodType(CreatePayPeriodTypeRequest request, CancellationToken cancellationToken)
    {
        var command = new CreatePayPeriodTypeCommand(
             request.Code,
             request.Name,
             request.Days,
             request.Description,
             request.CreatedBy
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpPut]
    [GeneratePermission]
    [RequirePermission("PayPeriodType.UpdatePayPeriodType")]
    public async Task<IActionResult> UpdatePayPeriodType(UpdatePayPeriodTypeRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdatePayPeriodTypeCommand(
             request.Id,
             request.Code,
             request.Name,
             request.Days,
             request.Description,
             request.UpdatedBy
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpDelete]
    [GeneratePermission]
    [RequirePermission("PayPeriodType.DeletePayPeriodType")]
    public async Task<IActionResult> DeletePayPeriodType(DeletePayPeriodTypeRequest request, CancellationToken cancellationToken)
    {
        var command = new DeletePayPeriodTypeCommand(
             request.Id,
             request.DeletedBy
         );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpGet]
    [GeneratePermission]
    [RequirePermission("PayPeriodType.GetPayPeriodType")]
    public async Task<IActionResult> GetPayPeriodTypes(
       [FromQuery] BaseSpecParams specParams,
       CancellationToken cancellationToken
    )
    {
        var query = new GetPayPeriodTypesQuery(specParams);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }


    [HttpGet("{id:long}")]
    [GeneratePermission]
    [RequirePermission("PayPeriodType.GetPayPeriodType")]
    public async Task<IActionResult> GetPayPeriodType(
        long id,
        CancellationToken cancellationToken
     )
    {
        var query = new GetPayPeriodTypeQuery(id);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }

}
