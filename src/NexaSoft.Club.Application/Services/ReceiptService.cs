using NexaSoft.Club.Application.Abstractions.Reporting;
using NexaSoft.Club.Domain.Features.Payments;

namespace NexaSoft.Club.Application.Services;

public class ReceiptService(IReceiptGenerator _receiptGenerator) : IReceiptService
{
  public byte[] GeneratePaymentReceipt(PaymentResponse payment, Action<ReceiptConfiguration>? configure = null)
    {
        var config = _receiptGenerator.GetDefaultConfiguration();
        
        // Personalizar configuración si se proporciona
        configure?.Invoke(config);
        
        return _receiptGenerator.GeneratePaymentReceipt(payment, config);
    }

    public byte[] GenerateStandardReceipt(PaymentResponse payment)
    {
        return GeneratePaymentReceipt(payment);
    }

    public byte[] GenerateMinimalReceipt(PaymentResponse payment)
    {
        return GeneratePaymentReceipt(payment, config =>
        {
            config.ShowQRCode = false;
            config.ShowBarcode = false;
            config.ShowWatermark = false;
            config.ShowPaymentDetails = false;
        });
    }

    public byte[] GeneratePremiumReceipt(PaymentResponse payment)
    {
        return GeneratePaymentReceipt(payment, config =>
        {
            config.PrimaryColor = "#2c3e50";
            config.SecondaryColor = "#3498db";
            config.AccentColor = "#e74c3c";
            config.ShowQRCode = true;
            config.ShowBarcode = true;
            config.WatermarkText = "COMPROBANTE VÁLIDO";
            config.ThankYouMessage = "¡Gracias por confiar en nosotros!";
        });
    }

    public ReceiptConfiguration GetDefaultConfiguration()
    {
        return _receiptGenerator.GetDefaultConfiguration();
    }
}
