using NexaSoft.Club.Domain.Features.Payments;
using QuestPDF.Helpers;

namespace NexaSoft.Club.Application.Abstractions.Reporting;

public interface IReceiptGenericService
{
    byte[] GeneratePaymentReceipt(PaymentResponse payment, Action<GenericReceiptConfig>? configure = null);
    byte[] GenerateA4Receipt(PaymentResponse payment);
    byte[] GenerateA5Receipt(PaymentResponse payment);
    byte[] GenerateCompactReceipt(PaymentResponse payment);
    GenericReceiptConfig GetDefaultConfiguration();
}


public class GenericReceiptConfig
{
    // Configuración del club
    public string ClubName { get; set; } = "CLUB CENTRO SOCIAL ICA";
    public string ClubSlogan { get; set; } = "El club de todos los socios";
    public string ClubAddress { get; set; } = "Calle Bolivar Nº166 Plaza de Armas - Ica";
    public string ClubPhone { get; set; } = "056-219198 / 231411";
    public string ClubEmail { get; set; } = "info@clubica.com";
    public string ClubWebsite { get; set; } = "www.clubica.com";

    // Configuración de formato
    public PageSize PageSize { get; set; } = PageSizes.A4;
    public int FontSizeNormal { get; set; } = 11;
    public int FontSizeSmall { get; set; } = 9;
    public int FontSizeLarge { get; set; } = 14;

    // Elementos visibles (más completos para formatos grandes)
    public bool ShowLogo { get; set; } = true;
    public bool ShowHeader { get; set; } = true;
    public bool ShowMemberDetails { get; set; } = true;
    public bool ShowItemsTable { get; set; } = true;
    public bool ShowPaymentDetails { get; set; } = true;
    public bool ShowFooter { get; set; } = true;
    public bool ShowWatermark { get; set; } = true;

    // Estilo
    public string PrimaryColor { get; set; } = "#1a3a5f";
    public string SecondaryColor { get; set; } = "#2c5c8a";
    public string AccentColor { get; set; } = "#e74c3c";
}
