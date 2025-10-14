using MediatR;
using Microsoft.AspNetCore.Mvc;
using NexaSoft.Club.Api.Controllers.Masters.PaymentTypes.Request;
using NexaSoft.Club.Application.Masters.PaymentTypes.Commands.CreatePaymentType;
using NexaSoft.Club.Application.Masters.PaymentTypes.Commands.UpdatePaymentType;
using NexaSoft.Club.Application.Masters.PaymentTypes.Commands.DeletePaymentType;
using NexaSoft.Club.Application.Masters.PaymentTypes.Queries.GetPaymentType;
using NexaSoft.Club.Application.Masters.PaymentTypes.Queries.GetPaymentTypes;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Api.Extensions;

namespace NexaSoft.Club.Api.Controllers.Masters.PaymentTypes;

[Route("api/[controller]")]
[ApiController]
public class PaymentTypeController(ISender _sender) : ControllerBase
{

    [HttpPost]
   public async Task<IActionResult> CreatePaymentType(CreatePaymentTypeRequest request, CancellationToken cancellationToken)
    {
        var command = new CreatePaymentTypeCommand(
             request.Name,
             request.Description,
    request.CreatedBy
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpPut]
   public async Task<IActionResult> UpdatePaymentType(UpdatePaymentTypeRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdatePaymentTypeCommand(
           request.Id,
             request.Name,
             request.Description,
             request.UpdatedBy
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpDelete]
   public async Task<IActionResult> DeletePaymentType(DeletePaymentTypeRequest request, CancellationToken cancellationToken)
    {
        var command = new DeletePaymentTypeCommand(
             request.Id,
             request.DeletedBy
         );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpGet]
    public async Task<IActionResult> GetPaymentTypes(
       [FromQuery] BaseSpecParams specParams,
       CancellationToken cancellationToken
    )
    {
        var query = new GetPaymentTypesQuery(specParams);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }


   [HttpGet("{id:long}")]
   public async Task<IActionResult> GetPaymentType(
       long id,
       CancellationToken cancellationToken
    )
    {
        var query = new GetPaymentTypeQuery(id);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }

}
