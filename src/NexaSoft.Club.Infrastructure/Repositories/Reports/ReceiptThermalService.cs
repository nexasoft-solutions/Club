using NexaSoft.Club.Application.Abstractions.Reporting;
using NexaSoft.Club.Domain.Features.Payments;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace NexaSoft.Club.Infrastructure.Repositories.Reports;

public class ReceiptThermalService : IReceiptThermalService
{
    public byte[] GeneratePaymentReceipt(PaymentResponse payment, Action<ThermalReceiptConfig>? configure = null)
    {
        var config = GetDefaultConfiguration();
        configure?.Invoke(config);

        return GenerateThermalReceipt(payment, config);
    }

    public byte[] GenerateStandardReceipt(PaymentResponse payment)
    {
        return GeneratePaymentReceipt(payment);
    }

    public byte[] GenerateMinimalReceipt(PaymentResponse payment)
    {
        return GeneratePaymentReceipt(payment, config =>
        {
            config.ShowHeader = false;
            config.ShowFooter = false;
        });
    }

    public ThermalReceiptConfig GetDefaultConfiguration()
    {
        return new ThermalReceiptConfig();
    }

    private byte[] GenerateThermalReceipt(PaymentResponse payment, ThermalReceiptConfig config)
    {
        // Aumentar ligeramente el ancho para mejor ajuste
        var thermal80mmWidth = 72 * 3.3f; // De 3.15 a 3.3 pulgadas
        var thermal80mmHeight = 72 * 11.7f;

        var thermalPageSize = new PageSize(thermal80mmWidth, thermal80mmHeight);

        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(thermalPageSize);
                page.Margin(3); // Reducir márgenes aún más
                page.PageColor(Colors.White);
                page.DefaultTextStyle(x => x
                    .FontSize(config.FontSizeNormal)
                    .FontFamily("Courier New")
                    .LineHeight(0.8f));

                page.Content().Element(compose => ComposeThermalContent(compose, payment, config));
            });
        });

        return document.GeneratePdf();
    }

    private void ComposeThermalContent(IContainer container, PaymentResponse payment, ThermalReceiptConfig config)
    {
        container.Column(column =>
        {
            // ENCABEZADO CENTRADO - TEXTO MÁS CORTO
            if (config.ShowHeader)
            {
                column.Item().AlignCenter().Text("CLUB CENTRO SOCIAL ICA").FontSize(config.FontSizeLarge).Bold();
                column.Item().AlignCenter().Text("El club de todos los socios").FontSize(config.FontSizeSmall);
                column.Item().AlignCenter().Text("Calle Bolivar 166 - Ica").FontSize(config.FontSizeSmall);
                column.Item().AlignCenter().Text("Tel: 056-219198").FontSize(config.FontSizeSmall);
                column.Item().AlignCenter().Text($"RUC: {config.ClubRUC}").FontSize(config.FontSizeSmall);

                if (config.ShowSeparators)
                {
                    column.Item().PaddingVertical(1).LineHorizontal(1);
                }
            }

            // INFORMACIÓN DEL COMPROBANTE - MÁS COMPACTO
            column.Item().PaddingBottom(1).Row(row =>
            {
                row.ConstantItem(40).AlignLeft().Text("COMPROBANTE:").SemiBold();
                row.RelativeItem().AlignLeft().Text(payment.ReceiptNumber ?? "N/A").SemiBold();
            });

            column.Item().Row(row =>
            {
                row.ConstantItem(40).AlignLeft().Text("FECHA:");
                row.RelativeItem().AlignLeft().Text(payment.PaymentDate.ToString("dd/MM/yyyy"));
            });

            column.Item().Row(row =>
            {
                row.ConstantItem(40).AlignLeft().Text("HORA:");
                row.RelativeItem().AlignLeft().Text(DateTime.Now.ToString("HH:mm:ss"));
            });

            column.Item().Row(row =>
            {
                row.ConstantItem(40).AlignLeft().Text("DOCUMENTO:");
                row.RelativeItem().AlignLeft().Text("Boleta");
            });

            column.Item().PaddingBottom(1).Row(row =>
            {
                row.ConstantItem(40).AlignLeft().Text("METODO:");
                row.RelativeItem().AlignLeft().Text("Efectivo");
            });

            if (config.ShowSeparators)
            {
                column.Item().PaddingVertical(1).LineHorizontal(1);
            }

            // MIEMBRO
            column.Item().PaddingBottom(1).Text("MIEMBRO:").SemiBold();
            column.Item().PaddingBottom(1).Text(payment.MemberFullName ?? "");

            if (config.ShowSeparators)
            {
                column.Item().PaddingVertical(1).LineHorizontal(1);
            }

            // DETALLE DE PAGOS - FORMATO MÁS SIMPLE
            if (config.ShowItems && payment.AppliedItems.Any())
            {
                column.Item().PaddingBottom(1).Text("DETALLE DE PAGOS:").SemiBold();

                foreach (var item in payment.AppliedItems)
                {
                    column.Item().Row(row =>
                    {
                        row.RelativeItem(3).Text(item.Concept ?? "");
                        row.ConstantItem(25).AlignRight().Text(item.Amount.ToString("C"));
                    });
                }

                if (config.ShowSeparators)
                {
                    column.Item().PaddingVertical(1).LineHorizontal(1);
                }
            }

            // TOTAL
            column.Item().Row(row =>
            {
                row.RelativeItem().Text("TOTAL:").SemiBold().FontSize(config.FontSizeLarge);
                row.ConstantItem(35).AlignRight().Text(payment.TotalAmount.ToString("C"))
                    .SemiBold().FontSize(config.FontSizeLarge);
            });

            // PIE DE PÁGINA SIMPLIFICADO
            if (config.ShowFooter)
            {
                column.Item().PaddingTop(2).AlignCenter().Text("GRACIAS POR SU PAGO!").SemiBold();
                column.Item().AlignCenter().Text(DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
            }
        });
    }

    private string TruncateText(string text, int maxLength)
    {
        if (string.IsNullOrEmpty(text)) return "";
        return text.Length <= maxLength ? text : text.Substring(0, maxLength - 3) + "...";
    }

    private List<string> SplitTextIntoLines(string text, int maxLineLength)
    {
        var lines = new List<string>();
        if (string.IsNullOrEmpty(text))
        {
            lines.Add("");
            return lines;
        }

        var words = text.Split(' ');
        var currentLine = "";

        foreach (var word in words)
        {
            if (currentLine.Length + word.Length + 1 <= maxLineLength)
            {
                currentLine += (currentLine == "" ? "" : " ") + word;
            }
            else
            {
                if (currentLine != "")
                    lines.Add(currentLine);

                currentLine = word.Length > maxLineLength ? word.Substring(0, maxLineLength - 3) + "..." : word;
            }
        }

        if (currentLine != "")
            lines.Add(currentLine);

        return lines;
    }

    private string ConvertAmountToText(decimal amount)
    {
        if (amount == 0) return "CERO CON 00/100 SOLES";

        var integerPart = (int)Math.Truncate(amount);
        var decimalPart = (int)((amount - integerPart) * 100);

        if (integerPart <= 10)
        {
            var integerText = integerPart switch
            {
                0 => "CERO",
                1 => "UNO",
                2 => "DOS",
                3 => "TRES",
                4 => "CUATRO",
                5 => "CINCO",
                6 => "SEIS",
                7 => "SIETE",
                8 => "OCHO",
                9 => "NUEVE",
                10 => "DIEZ",
                _ => integerPart.ToString()
            };
            return $"{integerText} CON {decimalPart:00}/100 SOLES";
        }

        return $"{integerPart} CON {decimalPart:00}/100 SOLES";
    }
}