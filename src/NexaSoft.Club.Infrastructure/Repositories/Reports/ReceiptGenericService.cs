using NexaSoft.Club.Application.Abstractions.Reporting;
using NexaSoft.Club.Domain.Features.Payments;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace NexaSoft.Club.Infrastructure.Repositories.Reports;

public class ReceiptGenericService: IReceiptGenericService
{
    public byte[] GeneratePaymentReceipt(PaymentResponse payment, Action<GenericReceiptConfig>? configure = null)
    {
        var config = GetDefaultConfiguration();
        configure?.Invoke(config);
        
        return GenerateGenericReceipt(payment, config);
    }

    public byte[] GenerateA4Receipt(PaymentResponse payment)
    {
        return GeneratePaymentReceipt(payment, config =>
        {
            config.PageSize = PageSizes.A4;
            config.ShowLogo = true;
            config.ShowMemberDetails = true;
            config.ShowPaymentDetails = true;
        });
    }

    public byte[] GenerateA5Receipt(PaymentResponse payment)
    {
        return GeneratePaymentReceipt(payment, config =>
        {
            config.PageSize = PageSizes.A5;
            config.FontSizeNormal = 10;
            config.FontSizeSmall = 8;
            config.FontSizeLarge = 12;
        });
    }

    public byte[] GenerateCompactReceipt(PaymentResponse payment)
    {
        return GeneratePaymentReceipt(payment, config =>
        {
            config.PageSize = PageSizes.A5;
            config.ShowLogo = false;
            config.ShowWatermark = false;
            config.ShowPaymentDetails = false;
        });
    }

    public GenericReceiptConfig GetDefaultConfiguration()
    {
        return new GenericReceiptConfig();
    }

    private byte[] GenerateGenericReceipt(PaymentResponse payment, GenericReceiptConfig config)
    {
        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Margin(25);
                page.Size(config.PageSize);
                page.PageColor(Colors.White);
                page.DefaultTextStyle(x => x.FontSize(config.FontSizeNormal));

                // Header
                if (config.ShowHeader)
                {
                    page.Header().Element(compose => ComposeHeader(compose, payment, config));
                }

                // Content
                page.Content().Element(compose => ComposeContent(compose, payment, config));

                // Footer
                if (config.ShowFooter)
                {
                    page.Footer().Element(compose => ComposeFooter(compose, config));
                }
            });
        });

        return document.GeneratePdf();
    }

    private void ComposeHeader(IContainer container, PaymentResponse payment, GenericReceiptConfig config)
    {
        container.Column(column =>
        {
            column.Item().AlignCenter().Text(config.ClubName).FontSize(config.FontSizeLarge).Bold().FontColor(config.PrimaryColor);
            column.Item().AlignCenter().Text(config.ClubSlogan).FontSize(config.FontSizeSmall).FontColor(config.SecondaryColor);
            column.Item().AlignCenter().Text("COMPROBANTE DE PAGO").FontSize(16).Bold().FontColor(config.PrimaryColor);
        });
    }

    private void ComposeContent(IContainer container, PaymentResponse payment, GenericReceiptConfig config)
    {
        container.Column(column =>
        {
            // Información básica
            column.Item().PaddingVertical(10).Row(row =>
            {
                row.RelativeItem().Column(col =>
                {
                    col.Item().Text($"Comprobante: {payment.ReceiptNumber}").SemiBold();
                    col.Item().Text($"Fecha: {payment.PaymentDate:dd/MM/yyyy}");
                    col.Item().Text($"Documento: {payment.DocumentTypeDescription}");
                });
                row.RelativeItem().Column(col =>
                {
                    col.Item().Text($"Método: {payment.PaymentMethodDescription}").SemiBold();
                    col.Item().Text($"Asiento: {payment.EntryNumber}");
                    col.Item().Text($"Estado: {payment.StatusDescription}");
                });
            });

            // Información del miembro
            if (config.ShowMemberDetails)
            {
                column.Item().PaddingVertical(10).Background(config.SecondaryColor).Padding(8).Text("INFORMACIÓN DEL MIEMBRO")
                    .FontColor(Colors.White).Bold();
                
                column.Item().PaddingVertical(5).Text(payment.MemberFullName).SemiBold().FontSize(config.FontSizeLarge);
            }

            // Tabla de items
            if (config.ShowItemsTable && payment.AppliedItems.Any())
            {
                column.Item().PaddingVertical(10).Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.RelativeColumn(3); // Concepto
                        columns.ConstantColumn(100); // Periodo
                        columns.ConstantColumn(100); // Monto
                    });

                    table.Header(header =>
                    {
                        header.Cell().Background(config.PrimaryColor).Padding(8).Text("CONCEPTO")
                            .FontColor(Colors.White).Bold();
                        header.Cell().Background(config.PrimaryColor).Padding(8).Text("PERIODO")
                            .FontColor(Colors.White).Bold();
                        header.Cell().Background(config.PrimaryColor).Padding(8).Text("MONTO")
                            .FontColor(Colors.White).Bold();
                    });

                    foreach (var item in payment.AppliedItems)
                    {
                        table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).Padding(8)
                            .Text(item.Concept);
                        table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).Padding(8)
                            .Text(item.Period);
                        table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).Padding(8)
                            .Text(item.Amount.ToString("C")).SemiBold();
                    }

                    // Total
                    table.Cell().ColumnSpan(2).BorderBottom(1).BorderColor(Colors.Grey.Lighten2)
                        .Padding(8).Text("TOTAL").Bold().AlignRight();
                    table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).Padding(8)
                        .Text(payment.TotalAmount.ToString("C")).Bold().FontColor(config.AccentColor);
                });
            }

            // Información adicional de pago
            if (config.ShowPaymentDetails)
            {
                column.Item().PaddingVertical(10).Background(Colors.Grey.Lighten3).Padding(10).Column(details =>
                {
                    details.Item().Text("INFORMACIÓN ADICIONAL").Bold().FontColor(config.PrimaryColor);
                    details.Item().Text($"Total Pagado: {payment.TotalAmount:C}");
                    details.Item().Text($"Fecha de Proceso: {DateTime.Now:dd/MM/yyyy HH:mm}");
                    details.Item().Text($"Generado por: {payment.CreatedBy ?? "Sistema"}");
                });
            }
        });
    }

    private void ComposeFooter(IContainer container, GenericReceiptConfig config)
    {
        container.Column(column =>
        {
            column.Item().PaddingTop(10).LineHorizontal(1).LineColor(Colors.Grey.Lighten2);
            column.Item().PaddingTop(5).AlignCenter().Text($"{config.ClubAddress} | {config.ClubPhone}")
                .FontSize(config.FontSizeSmall);
            column.Item().AlignCenter().Text($"{config.ClubEmail} | {config.ClubWebsite}")
                .FontSize(config.FontSizeSmall);
            column.Item().AlignCenter().Text($"Generado el {DateTime.Now:dd/MM/yyyy HH:mm}")
                .FontSize(config.FontSizeSmall - 1).FontColor(Colors.Grey.Medium);
        });
    }
}