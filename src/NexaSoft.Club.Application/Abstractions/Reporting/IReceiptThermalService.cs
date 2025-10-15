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
    // Configuración del club (MINIMALISTA)
    public string ClubName { get; set; } = "CLUB CENTRO SOCIAL ICA";
    public string ClubAddress { get; set; } = "Calle Bolivar 166 - Ica";
    public string ClubRUC { get; set; } = "20123456781";

    // Configuración optimizada para térmicas
    public int PaperWidth { get; set; } = 230; // 80mm exacto
    public int FontSizeNormal { get; set; } = 8;
    public int FontSizeSmall { get; set; } = 7;
    public int FontSizeLarge { get; set; } = 9;
    public int MaxCharsPerLine { get; set; } = 38; // Menos caracteres

    // Elementos visibles (SIMPLIFICADOS)
    public bool ShowHeader { get; set; } = true;
    public bool ShowItems { get; set; } = true;
    public bool ShowFooter { get; set; } = true;
    public bool ShowSeparators { get; set; } = true;
}