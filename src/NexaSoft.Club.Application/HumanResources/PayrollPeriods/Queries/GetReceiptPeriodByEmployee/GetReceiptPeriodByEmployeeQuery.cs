using NexaSoft.Club.Application.Abstractions.Messaging;
using QuestPDF.Helpers;

namespace NexaSoft.Club.Application.HumanResources.PayrollPeriods.Queries.GetReceiptPeriodByEmployee;

public sealed record GetReceiptPeriodByEmployeeQuery
(
    long PeriodDetailId
):IQuery<byte[]>;

public class PayrollReceiptConfig
{
    public PageSize PageSize { get; set; } = PageSizes.A4;
    public bool ShowHeader { get; set; } = true;
    public bool ShowFooter { get; set; } = true;
    public bool ShowLogo { get; set; } = true;
    public bool ShowWatermark { get; set; } = false;
    public bool ShowBreakdown { get; set; } = true;
    public bool ShowCompanyDetails { get; set; } = true;
    public bool ShowEmployeeDetails { get; set; } = true;
    public bool ShowLegalText { get; set; } = true;
    
    // Estilos
    public int FontSizeNormal { get; set; } = 9;
    public int FontSizeSmall { get; set; } = 8;
    public int FontSizeLarge { get; set; } = 11;
    public string PrimaryColor { get; set; } = "#1e40af"; // Azul profesional
    public string SecondaryColor { get; set; } = "#374151"; // Gris oscuro
    public string AccentColor { get; set; } = "#059669"; // Verde para totales
    
    // Información de la empresa
    public string CompanyName { get; set; } = "Club Social Ica";
    public string CompanyRUC { get; set; } = "20123456789";
    public string CompanyAddress { get; set; } = "Av. Ejemplo 123, Ica, Perú";
    public string CompanyPhone { get; set; } = "(056) 123-4567";
    public string CompanyEmail { get; set; } = "contacto@clubsocial.com.pe";
}
