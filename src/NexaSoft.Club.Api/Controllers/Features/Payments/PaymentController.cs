using MediatR;
using Microsoft.AspNetCore.Mvc;
using NexaSoft.Club.Api.Controllers.Features.Payments.Request;
using NexaSoft.Club.Application.Features.Payments.Commands.CreatePayment;
using NexaSoft.Club.Application.Features.Payments.Commands.DeletePayment;
using NexaSoft.Club.Application.Features.Payments.Queries.GetPayment;
using NexaSoft.Club.Application.Features.Payments.Queries.GetPayments;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Api.Extensions;
using NexaSoft.Club.Application.Features.Payments.Queries.GetReceiptByPayment;

namespace NexaSoft.Club.Api.Controllers.Features.Payments;

[Route("api/[controller]")]
[ApiController]
public class PaymentController(ISender _sender) : ControllerBase
{

    [HttpPost]
    public async Task<IActionResult> CreatePayment(CreatePaymentRequest request, CancellationToken cancellationToken)
    {
        var command = new CreatePaymentCommand(
             request.MemberId,
             request.PaymentItems?
                .Select(x => new PaymentItemDto(
                    x.MemberFeeId,
                    x.AmountToPay
                ))
                .ToList() ?? new List<PaymentItemDto>(), // si es null, se usa una lista vacía
             request.Amount,
             request.PaymentDate,
             request.PaymentMethodId,
             request.ReferenceNumber,
             request.DocumentTypeId,
             request.ReceiptNumber,
             request.CreatedBy
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

  

    [HttpDelete]
    public async Task<IActionResult> DeletePayment(DeletePaymentRequest request, CancellationToken cancellationToken)
    {
        var command = new DeletePaymentCommand(
             request.Id,
             request.DeletedBy
         );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpGet]
    public async Task<IActionResult> GetPayments(
       [FromQuery] BaseSpecParams specParams,
       CancellationToken cancellationToken
    )
    {
        var query = new GetPaymentsQuery(specParams);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }


    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetPayment(
        long id,
        CancellationToken cancellationToken
     )
    {
        var query = new GetPaymentQuery(id);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }

     // NUEVO MÉTODO PARA GENERAR COMPROBANTE
    [HttpGet("{id:long}/receipt")]
    public async Task<IActionResult> GetPaymentReceipt(
        long id,
        CancellationToken cancellationToken
    )
    {
        var query = new GetReceiptByPaymentQuery(id);
        var resultado = await _sender.Send(query, cancellationToken);

        if (resultado.IsSuccess)
        {
            // Obtener información del pago para el nombre del archivo
            var paymentQuery = new GetPaymentQuery(id);
            var paymentResult = await _sender.Send(paymentQuery, cancellationToken);
            
            var fileName = paymentResult.IsSuccess 
                ? $"Comprobante-{paymentResult.Value.ReceiptNumber}.pdf"
                : $"Comprobante-Pago-{id}.pdf";

            return File(resultado.Value, "application/pdf", fileName);
        }

        return resultado.ToActionResult(this);
    }

}
