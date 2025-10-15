using System.Globalization;
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
        var thermal80mmWidth = 72 * 3.15f; // 80mm EXACTO
        var thermalPageSize = new PageSize(thermal80mmWidth, 400);

        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(thermalPageSize);
                page.Margin(2); // Márgenes mínimos para térmicas
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
            // ENCABEZADO MINIMALISTA
            if (config.ShowHeader)
            {
                column.Item().AlignCenter().Text(config.ClubName).Bold();
                column.Item().AlignCenter().Text(config.ClubAddress).FontSize(config.FontSizeSmall);
                column.Item().AlignCenter().Text($"RUC: {config.ClubRUC}").FontSize(config.FontSizeSmall);

                if (config.ShowSeparators)
                {
                    column.Item().PaddingVertical(1).AlignCenter().Text(new string('-', 36));
                }
            }

            // INFORMACIÓN BÁSICA DEL COMPROBANTE
            column.Item().Row(row =>
            {
                row.ConstantItem(40).Text(payment.DocumentTypeDescription).SemiBold();
                row.RelativeItem().Text(payment.ReceiptNumber);
            });

            column.Item().Row(row =>
            {
                row.ConstantItem(40).Text("FECHA:");
                row.RelativeItem().Text(payment.CreatedAt.ToString("dd/MM/yyyy HH:mm"));
            });

            column.Item().Row(row =>
            {
                row.ConstantItem(40).Text("METODO:");
                row.RelativeItem().Text(payment.PaymentMethodDescription);
            });

            if (config.ShowSeparators)
            {
                column.Item().PaddingVertical(1).AlignCenter().Text(new string('-', 36));
            }

            // INFORMACIÓN DEL SOCIO
            column.Item().Row(row =>
            {
                row.ConstantItem(40).Text("SOCIO:").SemiBold();
                row.RelativeItem().Text(TruncateText(payment.MemberFullName ?? "", 30));
            });

            if (config.ShowSeparators)
            {
                column.Item().PaddingVertical(1).AlignCenter().Text(new string('=', 36));
            }

            // DETALLE DE PAGOS - OPTIMIZADO PARA 80mm
            if (config.ShowItems && payment.AppliedItems.Any())
            {
                column.Item().PaddingBottom(1).Text("DETALLE:").SemiBold();

                foreach (var item in payment.AppliedItems)
                {
                    column.Item().PaddingBottom(1).Row(row =>
                    {
                        // Concepto truncado a 22 caracteres + monto
                        row.RelativeItem().Text(
                            TruncateText(
                                item.Concept == "Cuota de Ingreso"
                                    ? (item.Concept ?? "")
                                    : $"Cuota {item.Period ?? ""}",
                                22)
                        );
                        row.ConstantItem(70).AlignRight().Text(item.Amount.ToString("C", CultureInfo.GetCultureInfo("es-PE")));
                    });
                }

                if (config.ShowSeparators)
                {
                    column.Item().PaddingVertical(1).AlignCenter().Text(new string('-', 36));
                }
            }

            // TOTAL - EN UNA SOLA LÍNEA
            column.Item().PaddingTop(1).Row(row =>
            {
                row.RelativeItem().Text("TOTAL:").SemiBold().FontSize(config.FontSizeLarge);
                row.ConstantItem(70).AlignRight().Text(
                        payment.TotalAmount.ToString("C", CultureInfo.GetCultureInfo("es-PE"))
                    )
                    .SemiBold().FontSize(config.FontSizeLarge);
            });

            // MONTO EN LETRAS
            column.Item().PaddingTop(2).AlignCenter().Text(ConvertAmountToText(payment.TotalAmount)).Italic().FontSize(config.FontSizeSmall);

            // PIE DE PÁGINA MÍNIMO
            if (config.ShowFooter)
            {
                column.Item().PaddingTop(4).AlignCenter().Text("GRACIAS POR SU PAGO").SemiBold();
                column.Item().AlignCenter().Text(DateTime.Now.ToString("dd/MM/yy HH:mm"));
            }

            // LÍNEA DE CORTE
            column.Item().PaddingTop(3).AlignCenter().Text(new string('-', 36));
            column.Item().AlignCenter().Text("▲ CORTAR AQUÍ ▲").FontSize(config.FontSizeSmall);
        });
    }

    private string TruncateText(string text, int maxLength)
    {
        if (string.IsNullOrEmpty(text)) return "";
        return text.Length <= maxLength ? text : text.Substring(0, maxLength - 1);
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