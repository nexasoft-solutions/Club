using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Reporting;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Features.Payments;
using NexaSoft.Club.Domain.Specifications;

namespace NexaSoft.Club.Application.Features.Payments.Queries.GetReceiptByPayment;

public class GetReceiptByPaymentQueryHandler(
    IGenericRepository<Payment> _paymentRepository,
    IReceiptThermalService _receiptThermalService
) : IQueryHandler<GetReceiptByPaymentQuery, byte[]>
{

    public async Task<Result<byte[]>> Handle(GetReceiptByPaymentQuery query, CancellationToken cancellationToken)
    {
        var specParams = new BaseSpecParams { Id = query.PaymentId };
        var spec = new PaymentSpecification(specParams);
        var payment = await _paymentRepository.GetEntityWithSpec(spec, cancellationToken);

        if (payment is null)
            return Result.Failure<byte[]>(PaymentErrores.NoEncontrado);

        try
        {
            // Usar configuración optimizada para térmicas
            var pdfBytes = _receiptThermalService.GeneratePaymentReceipt(payment, config =>
            {
                config.FontSizeNormal = 7;
                config.FontSizeSmall = 6;
                config.FontSizeLarge = 8;
                config.MaxCharsPerLine = 38;
                config.PaperWidth = 226; // 80mm en puntos
            });

            return Result.Success(pdfBytes);

        }
        catch (Exception ex)
        {
            return Result.Failure<byte[]>(new Error("Receipt.GenerationError", $"Error generando comprobante: {ex.Message}"));
        }
    }
}

