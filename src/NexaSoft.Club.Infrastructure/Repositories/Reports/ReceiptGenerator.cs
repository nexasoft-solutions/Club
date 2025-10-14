using NexaSoft.Club.Application.Abstractions.Reporting;
using NexaSoft.Club.Domain.Features.Payments;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace NexaSoft.Club.Infrastructure.Repositories.Reports;

public class ReceiptGenerator: IReceiptGenerator
{
     public byte[] GeneratePaymentReceipt(PaymentResponse payment, ReceiptConfiguration config)
    {
        var receiptData = MapToReceiptData(payment);
        
        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Margin(20);
                page.Size(config.PageSize);
                page.PageColor(Colors.White);
                page.DefaultTextStyle(x => x.FontSize(10).FontFamily("Helvetica"));

                // Header con logo y información del club
                page.Header().Element(compose => ComposeHeader(compose, config, receiptData));
                
                // Contenido principal
                page.Content().Element(compose => ComposeContent(compose, receiptData, config));
                
                // Footer único - combinar todo en un solo footer
                page.Footer().Element(compose => ComposeFooter(compose, config));
            });
        });

        return document.GeneratePdf();
    }

    private void ComposeHeader(IContainer container, ReceiptConfiguration config, ReceiptData data)
    {
        container.Column(column =>
        {
            // Primera fila: Logo e información del club
            column.Item().Row(row =>
            {
                // Logo
                if (!string.IsNullOrEmpty(config.LogoPath) && File.Exists(config.LogoPath))
                {
                    row.ConstantItem(config.LogoWidth).Height(config.LogoHeight)
                       .Image(config.LogoPath);
                }
                else
                {
                    // Logo placeholder si no existe
                    row.ConstantItem(config.LogoWidth).Height(config.LogoHeight)
                       .Background(config.PrimaryColor).AlignCenter().AlignMiddle()
                       .Text("NS").FontColor(Colors.White).Bold().FontSize(16);
                }

                // Información del club
                row.RelativeItem().PaddingLeft(10).Column(clubColumn =>
                {
                    clubColumn.Item().Text(config.ClubName).FontSize(16).Bold().FontColor(config.PrimaryColor);
                    clubColumn.Item().Text(config.ClubSlogan).FontSize(10).FontColor(config.SecondaryColor);
                    clubColumn.Item().Text(config.ClubAddress).FontSize(8).FontColor(Colors.Grey.Darken1);
                    clubColumn.Item().Text($"{config.ClubPhone} | {config.ClubEmail}").FontSize(8).FontColor(Colors.Grey.Darken1);
                });

                // Número de comprobante y fecha
                row.ConstantItem(100).AlignRight().Column(infoColumn =>
                {
                    infoColumn.Item().Text($"# {data.ReceiptNumber}").FontSize(9).SemiBold().FontColor(config.PrimaryColor);
                    infoColumn.Item().Text($"Fecha: {data.PaymentDate}").FontSize(9).FontColor(Colors.Grey.Darken1);
                });
            });

            column.Item().PaddingTop(10).LineHorizontal(1).LineColor(config.PrimaryColor);
            
            // Título del comprobante
            column.Item().PaddingVertical(5).AlignCenter().Text(config.ReceiptTitle)
                .FontSize(14).Bold().FontColor(config.PrimaryColor);
        });
    }

    private void ComposeContent(IContainer container, ReceiptData data, ReceiptConfiguration config)
    {
        container.Column(column =>
        {
            // Información del comprobante
            column.Item().Background(config.LightColor).Padding(10).Border(1).BorderColor(Colors.Grey.Lighten2).Column(infoColumn =>
            {
                infoColumn.Item().Row(row =>
                {
                    row.RelativeItem().Column(leftCol =>
                    {
                        leftCol.Item().Text($"Comprobante: {data.ReceiptNumber}").FontSize(10).SemiBold();
                        leftCol.Item().Text($"Fecha: {data.PaymentDate}").FontSize(10);
                        leftCol.Item().Text($"Asiento: {data.EntryNumber}").FontSize(10);
                    });
                    
                    row.RelativeItem().Column(rightCol =>
                    {
                        rightCol.Item().Text($"Documento: {data.DocumentType}").FontSize(10);
                        rightCol.Item().Text($"Método: {data.PaymentMethod}").FontSize(10);
                        rightCol.Item().Text($"Cajero: {data.CreatedBy}").FontSize(10);
                    });
                });
            });

            // Información del miembro
            if (config.ShowMemberInfo)
            {
                column.Item().PaddingTop(10).Row(row =>
                {
                    row.RelativeItem().Background(config.SecondaryColor).Padding(8).Text("MIEMBRO")
                        .FontColor(Colors.White).Bold().FontSize(11);
                });
                
                column.Item().PaddingVertical(5).Text(data.MemberName).FontSize(11).SemiBold();
            }

            // Detalles de items de pago
            if (config.ShowItemDetails && data.Items.Any())
            {
                column.Item().PaddingTop(10).Row(row =>
                {
                    row.RelativeItem().Background(config.SecondaryColor).Padding(8).Text("DETALLE DE PAGOS")
                        .FontColor(Colors.White).Bold().FontSize(11);
                });

                column.Item().PaddingTop(5).Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.RelativeColumn(3); // Concepto
                        columns.ConstantColumn(80); // Periodo
                        columns.ConstantColumn(80); // Monto
                    });

                    // Encabezado de la tabla
                    table.Header(header =>
                    {
                        header.Cell().Background(config.LightColor).Padding(5).Text("CONCEPTO")
                            .FontSize(9).Bold().FontColor(config.PrimaryColor);
                        header.Cell().Background(config.LightColor).Padding(5).Text("PERIODO")
                            .FontSize(9).Bold().FontColor(config.PrimaryColor);
                        header.Cell().Background(config.LightColor).Padding(5).Text("MONTO")
                            .FontSize(9).Bold().FontColor(config.PrimaryColor);
                    });

                    // Items
                    foreach (var item in data.Items)
                    {
                        table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).Padding(5)
                            .Text(item.Concept).FontSize(9);
                        table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).Padding(5)
                            .Text(item.Period).FontSize(9);
                        table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).Padding(5)
                            .Text(item.FormattedAmount).FontSize(9).SemiBold();
                    }

                    // Total
                    table.Cell().ColumnSpan(2).BorderBottom(1).BorderColor(Colors.Grey.Lighten2)
                        .Padding(5).Text("TOTAL").FontSize(10).Bold().AlignRight();
                    table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).Padding(5)
                        .Text(data.FormattedTotal).FontSize(10).Bold().FontColor(config.AccentColor);
                });
            }

            // Detalles adicionales del pago
            if (config.ShowPaymentDetails)
            {
                column.Item().PaddingTop(15).Background(config.LightColor).Padding(10).Column(detailsColumn =>
                {
                    detailsColumn.Item().Text("INFORMACIÓN ADICIONAL").FontSize(10).Bold().FontColor(config.PrimaryColor);
                    detailsColumn.Item().PaddingTop(5).Text($"Total Pagado: {data.FormattedTotal}").FontSize(9);
                    detailsColumn.Item().Text($"Fecha de Proceso: {DateTime.Now:dd/MM/yyyy HH:mm}").FontSize(9);
                    detailsColumn.Item().Text($"Estado: Procesado Exitosamente").FontSize(9).FontColor(Colors.Green.Darken1);
                });
            }

            // Código QR y código de barras
            /*if (config.ShowQRCode || config.ShowBarcode)
            {
                column.Item().PaddingTop(15).Row(row =>
                {
                    if (config.ShowQRCode)
                    {
                        row.RelativeItem().AlignCenter().Column(qrColumn =>
                        {
                            qrColumn.Item().Width(80).Height(80).Canvas((canvas, size) =>
                            {
                                DrawQRPlaceholder(canvas, size, data.ReceiptNumber);
                            });
                            qrColumn.Item().PaddingTop(2).Text("Código QR").FontSize(7).AlignCenter();
                        });
                    }

                    if (config.ShowBarcode)
                    {
                        row.RelativeItem().AlignCenter().Column(barcodeColumn =>
                        {
                            barcodeColumn.Item().Height(30).Canvas((canvas, size) =>
                            {
                                DrawBarcodePlaceholder(canvas, size, data.ReceiptNumber);
                            });
                            barcodeColumn.Item().PaddingTop(2).Text(data.ReceiptNumber).FontSize(7).AlignCenter();
                        });
                    }
                });
            }*/

            // Mensaje de agradecimiento
            column.Item().PaddingTop(20).AlignCenter().Text(config.ThankYouMessage)
                .FontSize(11).Italic().FontColor(config.SecondaryColor);

            // Watermark opcional en el contenido si es necesario
            if (config.ShowWatermark)
            {
                column.Item().PaddingTop(10).AlignCenter().Text(config.WatermarkText)
                    .FontSize(8).Italic().FontColor(Colors.Grey.Lighten1);
            }
        });
    }

    private void ComposeFooter(IContainer container, ReceiptConfiguration config)
    {
        container.Column(column =>
        {
            column.Item().PaddingTop(10).LineHorizontal(1).LineColor(Colors.Grey.Lighten2);
            column.Item().PaddingTop(5).AlignCenter().Text(config.FooterText)
                .FontSize(8).FontColor(Colors.Grey.Medium);
            column.Item().AlignCenter().Text($"{config.ClubWebsite} | Generado el {DateTime.Now:dd/MM/yyyy HH:mm}")
                .FontSize(7).FontColor(Colors.Grey.Lighten1);
            
            // Número de página en el footer
            column.Item().PaddingTop(5).AlignRight().Text(text =>
            {
                text.Span("Página ").FontSize(7);
                text.CurrentPageNumber().FontSize(7).Bold();
                text.Span(" de ").FontSize(7);
                text.TotalPages().FontSize(7).Bold();
            });
        });
    }

    private ReceiptData MapToReceiptData(PaymentResponse payment)
    {
        return new ReceiptData
        {
            ReceiptNumber = payment.ReceiptNumber ?? "N/A",
            PaymentDate = payment.PaymentDate.ToString("dd/MM/yyyy"),
            MemberName = payment.MemberFullName!,
            PaymentMethod = payment.PaymentMethodDescription!,
            DocumentType = payment.DocumentTypeDescription!,
            TotalAmount = payment.TotalAmount,
            EntryNumber = payment.EntryNumber!,
            CreatedBy = payment.CreatedBy ?? "Sistema",
            Items = payment.AppliedItems.Select(item => new ReceiptItem
            {
                Concept = item.Concept!,
                Period = item.Period!,
                Amount = item.Amount
            }).ToList()
        };
    }

    /*private void DrawQRPlaceholder(ICanvas canvas, Size size, string text)
    {
        // Placeholder para código QR
        canvas.DrawRectangle(0, 0, size.Width, size.Height);
        canvas.StrokeColor(Colors.Grey.Lighten1);
        canvas.Stroke();
        
        canvas.DrawRectangle(5, 5, size.Width - 10, size.Height - 10);
        canvas.StrokeColor(Colors.Grey.Lighten2);
        canvas.Stroke();
        
        canvas.DrawRectangle(10, 10, size.Width - 20, size.Height - 20);
        canvas.FillColor(Colors.Grey.Lighten3);
        canvas.Fill();
    }

    private void DrawBarcodePlaceholder(ICanvas canvas, Size size, string text)
    {
        // Placeholder para código de barras
        var barWidth = size.Width / (text.Length + 2);
        
        for (int i = 0; i < text.Length; i++)
        {
            if (i % 2 == 0)
            {
                canvas.DrawRectangle(i * barWidth, 0, barWidth, size.Height);
                canvas.FillColor(Colors.Black);
                canvas.Fill();
            }
        }
    }*/

    public ReceiptConfiguration GetDefaultConfiguration()
    {
        return new ReceiptConfiguration();
    }
}