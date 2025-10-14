using NexaSoft.Club.Domain.Features.Payments;
using QuestPDF.Helpers;

namespace NexaSoft.Club.Application.Abstractions.Reporting;

public interface IReceiptGenerator
{
    byte[] GeneratePaymentReceipt(PaymentResponse payment, ReceiptConfiguration config);
    ReceiptConfiguration GetDefaultConfiguration();
}


// Modelos de Configuración
public class ReceiptConfiguration
{
    public string ClubName { get; set; } = "NEXASOFT CLUB";
    public string ClubSlogan { get; set; } = "Excelencia y Desarrollo";
    public string ClubAddress { get; set; } = "Av. Principal 123, Lima, Perú";
    public string ClubPhone { get; set; } = "+51 987 654 321";
    public string ClubEmail { get; set; } = "info@nexasoftclub.com";
    public string ClubWebsite { get; set; } = "www.nexasoftclub.com";
    
    // Configuración de Logo
    public string LogoPath { get; set; } = "";
    public int LogoWidth { get; set; } = 60;
    public int LogoHeight { get; set; } = 60;
    
    // Colores del tema
    public string PrimaryColor { get; set; } = "#1a3a5f";
    public string SecondaryColor { get; set; } = "#2c5c8a";
    public string AccentColor { get; set; } = "#e74c3c";
    public string LightColor { get; set; } = "#f8f9fa";
    
    // Configuración del documento
    public PageSize PageSize { get; set; } = PageSizes.A5;
    public bool ShowWatermark { get; set; } = true;
    public string WatermarkText { get; set; } = "COMPROBANTE OFICIAL";
    
    // Textos personalizables
    public string ReceiptTitle { get; set; } = "COMPROBANTE DE PAGO";
    public string ThankYouMessage { get; set; } = "¡Gracias por su pago!";
    public string FooterText { get; set; } = "Este es un comprobante oficial generado electrónicamente";
    
    // Configuración de elementos opcionales
    public bool ShowQRCode { get; set; } = true;
    public bool ShowBarcode { get; set; } = true;
    public bool ShowPaymentDetails { get; set; } = true;
    public bool ShowItemDetails { get; set; } = true;
    public bool ShowMemberInfo { get; set; } = true;
}

public class ReceiptItem
{
    public string Concept { get; set; } = string.Empty;
    public string Period { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string FormattedAmount => Amount.ToString("C");
}

public class ReceiptData
{
    public string ReceiptNumber { get; set; } = string.Empty;
    public string PaymentDate { get; set; } = string.Empty;
    public string MemberName { get; set; } = string.Empty;
    public string PaymentMethod { get; set; } = string.Empty;
    public string DocumentType { get; set; } = string.Empty;
    public decimal TotalAmount { get; set; }
    public string FormattedTotal => TotalAmount.ToString("C");
    public List<ReceiptItem> Items { get; set; } = new();
    public string EntryNumber { get; set; } = string.Empty;
    public string CreatedBy { get; set; } = string.Empty;
}