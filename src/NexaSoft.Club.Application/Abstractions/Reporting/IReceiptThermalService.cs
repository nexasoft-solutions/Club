using NexaSoft.Club.Domain.Features.Payments;

namespace NexaSoft.Club.Application.Abstractions.Reporting;


public interface IReceiptThermalService
{
    byte[] GeneratePaymentReceipt(PaymentResponse payment, Action<ThermalReceiptConfig>? configure = null);
    byte[] GenerateStandardReceipt(PaymentResponse payment);
    byte[] GenerateMinimalReceipt(PaymentResponse payment);
    ThermalReceiptConfig GetDefaultConfiguration();
}

public class ThermalReceiptConfig
{
    // Configuración del club
    public string ClubName { get; set; } = "CLUB CENTRO SOCIAL ICA";
    public string ClubSlogan { get; set; } = "El club de todos los socios";
    public string ClubAddress { get; set; } = "Calle Bolivar Nº166 Plaza de Armas - Ica";
    public string ClubPhone { get; set; } = "056-219198 / 231411";
    public string ClubRUC { get; set; } = "20123456781";

   // Configuración optimizada para 80mm (ACTUALIZADA)
    public int PaperWidth { get; set; } = 226; // 80mm en puntos (72 * 3.15)
    public int FontSizeNormal { get; set; } = 7;  // Reducido para impresoras térmicas
    public int FontSizeSmall { get; set; } = 6;   // Más pequeño
    public int FontSizeLarge { get; set; } = 8;   // Reducido
    public int MaxCharsPerLine { get; set; } = 38; // Menos caracteres por línea

    // Elementos visibles
    public bool ShowHeader { get; set; } = true;
    public bool ShowItems { get; set; } = true;
    public bool ShowFooter { get; set; } = true;
    public bool ShowSeparators { get; set; } = true;
    public bool ShowCutLine { get; set; } = true;
}