
using NexaSoft.Club.Domain.Features.Payments;

namespace NexaSoft.Club.Application.Abstractions.Reporting;

public interface IReceiptService
{
    byte[] GeneratePaymentReceipt(PaymentResponse payment, Action<ReceiptConfiguration>? configure = null);
    byte[] GenerateStandardReceipt(PaymentResponse payment);
    byte[] GenerateMinimalReceipt(PaymentResponse payment);
    byte[] GeneratePremiumReceipt(PaymentResponse payment);
    ReceiptConfiguration GetDefaultConfiguration();
}
