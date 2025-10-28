using MediatR;
using Microsoft.AspNetCore.Mvc;
using NexaSoft.Club.Api.Controllers.HumanResources.PaymentMethodTypes.Request;
using NexaSoft.Club.Application.HumanResources.PaymentMethodTypes.Commands.CreatePaymentMethodType;
using NexaSoft.Club.Application.HumanResources.PaymentMethodTypes.Commands.UpdatePaymentMethodType;
using NexaSoft.Club.Application.HumanResources.PaymentMethodTypes.Commands.DeletePaymentMethodType;
using NexaSoft.Club.Application.HumanResources.PaymentMethodTypes.Queries.GetPaymentMethodType;
using NexaSoft.Club.Application.HumanResources.PaymentMethodTypes.Queries.GetPaymentMethodTypes;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Api.Extensions;

namespace NexaSoft.Club.Api.Controllers.HumanResources.PaymentMethodTypes;

[Route("api/[controller]")]
[ApiController]
public class PaymentMethodTypeController(ISender _sender) : ControllerBase
{

    [HttpPost]
   public async Task<IActionResult> CreatePaymentMethodType(CreatePaymentMethodTypeRequest request, CancellationToken cancellationToken)
    {
        var command = new CreatePaymentMethodTypeCommand(
             request.Code,
             request.Name,
             request.Description,
    request.CreatedBy
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpPut]
   public async Task<IActionResult> UpdatePaymentMethodType(UpdatePaymentMethodTypeRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdatePaymentMethodTypeCommand(
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
   public async Task<IActionResult> DeletePaymentMethodType(DeletePaymentMethodTypeRequest request, CancellationToken cancellationToken)
    {
        var command = new DeletePaymentMethodTypeCommand(
             request.Id,
             request.DeletedBy
         );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpGet]
    public async Task<IActionResult> GetPaymentMethodTypes(
       [FromQuery] BaseSpecParams specParams,
       CancellationToken cancellationToken
    )
    {
        var query = new GetPaymentMethodTypesQuery(specParams);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }


   [HttpGet("{id:long}")]
   public async Task<IActionResult> GetPaymentMethodType(
       long id,
       CancellationToken cancellationToken
    )
    {
        var query = new GetPaymentMethodTypeQuery(id);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }

}
