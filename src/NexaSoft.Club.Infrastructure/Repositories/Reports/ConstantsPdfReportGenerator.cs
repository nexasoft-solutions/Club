using NexaSoft.Club.Application.Masters.Constantes;
using NexaSoft.Club.Application.Masters.Constantes.Queries.GetConstesMultiple;
using QuestPDF.Fluent;
using QuestPDF.Helpers;


namespace NexaSoft.Club.Infrastructure.Repositories.Reports;

public class ConstantsPdfReportGenerator : IConstantsPdfReportGenerator
{
    public byte[] GenerateConstantsReport(
        IEnumerable<ConstantesAgrupadasResponse> constantes,
        string title = "Reporte de Constantes",
        string subtitle = "",
        string logoPath = "")
    {
        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Margin(25);
                page.Size(PageSizes.A4);
                page.PageColor(Colors.White);
                page.DefaultTextStyle(x => x.FontSize(10).FontFamily("Helvetica"));

                // Encabezado con logo
                page.Header().Height(70).Background(Colors.White).Row(row =>
                {
                    if (!string.IsNullOrEmpty(logoPath))
                    {
                        row.ConstantItem(50).Height(50).Image(logoPath);
                    }

                    row.RelativeItem().PaddingLeft(10).Column(column =>
                    {
                        column.Item().Text(title).FontSize(18).Bold().FontColor("#1a3a5f");
                        if (!string.IsNullOrEmpty(subtitle))
                        {
                            column.Item().PaddingTop(2).Text(subtitle).FontSize(10).FontColor(Colors.Grey.Darken1);
                        }
                    });

                    row.ConstantItem(80).AlignRight().Text(t =>
                    {
                        t.Span("Pág. ").FontSize(9);
                        t.CurrentPageNumber().FontSize(9).Bold();
                        t.Span(" de ").FontSize(9);
                        t.TotalPages().FontSize(9).Bold();
                    });
                });

                // Contenido principal - Constantes
                page.Content().PaddingTop(15).Column(column =>
                {
                    foreach (var constante in constantes)
                    {
                        // Tarjeta para cada tipo de constante
                        column.Item().Background(Colors.White).Border(1).BorderColor("#e0e0e0").Padding(15).Column(constanteColumn =>
                        {
                            // Encabezado del tipo de constante
                            constanteColumn.Item().PaddingBottom(10).Row(row =>
                            {
                                row.RelativeItem().Text(constante.Tipo).FontSize(12).Bold().FontColor("#1a3a5f");
                                row.AutoItem().Background("#1a3a5f").PaddingHorizontal(8).PaddingVertical(4)
                                    .Text($"{constante.Valores.Count()} registros").FontColor(Colors.White).FontSize(9);
                            });

                            constanteColumn.Item().PaddingBottom(10).LineHorizontal(1).LineColor("#e8e8e8");

                            // Tabla de valores de la constante
                            if (constante.Valores.Any())
                            {
                                constanteColumn.Item().Table(table =>
                                {
                                    // Definir columnas
                                    table.ColumnsDefinition(columns =>
                                    {
                                        columns.ConstantColumn(80);  // Clave
                                        columns.RelativeColumn();     // Valor
                                    });

                                    // Encabezados
                                    table.Header(header =>
                                    {
                                        header.Cell().Background("#f0f4f8").Padding(8).Text("Código")
                                            .FontSize(10).Bold().FontColor("#1a3a5f");
                                        header.Cell().Background("#f0f4f8").Padding(8).Text("Descripción")
                                            .FontSize(10).Bold().FontColor("#1a3a5f");
                                    });

                                    // Filas de valores (ordenadas por clave)
                                    foreach (var valor in constante.Valores.OrderBy(v => v.Clave))
                                    {
                                        table.Cell().BorderBottom(1).BorderColor("#e8e8e8")
                                            .Padding(8).Text(valor.Clave.ToString()).FontSize(10);
                                        table.Cell().BorderBottom(1).BorderColor("#e8e8e8")
                                            .Padding(8).Text(valor.Valor).FontSize(10);
                                    }
                                });
                            }
                            else
                            {
                                constanteColumn.Item().Padding(10).AlignCenter().Text("No hay valores definidos")
                                    .FontSize(10).Italic().FontColor(Colors.Grey.Medium);
                            }
                        });

                        column.Item().Height(10); // Espacio entre constantes
                    }
                });

                // Pie de página
                page.Footer().Height(25).Background(Colors.White).AlignCenter().Text(text =>
                {
                    text.Span("© ").FontSize(8).FontColor(Colors.Grey.Medium);
                    text.Span(DateTime.Now.Year.ToString()).FontSize(8).Bold().FontColor(Colors.Grey.Darken1);
                    text.Span(" NexaSoft Agro | ").FontSize(8).FontColor(Colors.Grey.Medium);
                    text.Span("Confidencial").FontSize(8).Italic().FontColor(Colors.Grey.Medium);
                });
            });
        });

        return document.GeneratePdf();
    }
}
