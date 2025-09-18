using System.Reflection;
using NexaSoft.Agro.Application.Abstractions.Reporting;
using NexaSoft.Agro.Domain.Abstractions;
using QuestPDF.Fluent;
using QuestPDF.Helpers;

namespace NexaSoft.Agro.Infrastructure.Repositories.Reports;

public class PdfReportGenerator<T> : IPdfReportGenerator<T>
{
    public byte[] Generate(
         IEnumerable<T> items,
         List<ColumnDefinition> columnDefinitions,
         string title = "Reporte",
         string subtitle = "",
         string logoPath = "",
         string entityName = "Registro",
         string? idPropertyName = "Id",
         string? highlightPropertyName = null,
         List<DetailSectionDefinition>? detailSections = null)
    {
       var sortedColumns = columnDefinitions.OrderBy(c => c.Order).ToList();
        var sortedDetailSections = detailSections?.OrderBy(d => d.Order).ToList();

        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Margin(25);
                page.Size(PageSizes.A4);
                page.PageColor(Colors.White);
                page.DefaultTextStyle(x => x.FontSize(10).FontFamily("Helvetica"));

                // Encabezado con logo (igual que antes)
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

                // Contenido principal
                page.Content().PaddingTop(15).Column(column =>
                {
                    foreach (var item in items)
                    {
                        var recordId = idPropertyName != null ? 
                            GetPropertyValue(item!, idPropertyName)?.ToString() ?? "N/A" : 
                            "N/A";

                        var highlightValue = GetHighlightValue(item, highlightPropertyName, sortedColumns);

                        // Tarjeta principal
                        column.Item().Background(Colors.White).Border(1).BorderColor("#e0e0e0").Padding(15).Column(entityColumn =>
                        {
                            // Encabezado
                            entityColumn.Item().PaddingBottom(10).Row(row =>
                            {
                                row.RelativeItem().Text($"{entityName} ID: {recordId}").FontSize(12).Bold().FontColor("#1a3a5f");
                                
                                if (!string.IsNullOrEmpty(highlightValue))
                                {
                                    row.AutoItem().Background("#1a3a5f").PaddingHorizontal(8).PaddingVertical(4)
                                        .Text(highlightValue).FontColor(Colors.White).FontSize(9);
                                }
                            });

                            entityColumn.Item().PaddingBottom(10).LineHorizontal(1).LineColor("#e8e8e8");

                            // Información principal
                            entityColumn.Item().Table(mainTable =>
                            {
                                mainTable.ColumnsDefinition(columns =>
                                {
                                    columns.ConstantColumn(120);
                                    columns.RelativeColumn();
                                });

                                foreach (var colDef in sortedColumns)
                                {
                                    var value = GetPropertyValue(item!, colDef.PropertyName);
                                    var displayValue = FormatValue(value, colDef.PropertyName, colDef.ValueFormat);
                                    
                                    mainTable.Cell().PaddingBottom(8).Text($"{colDef.DisplayName}:")
                                        .FontSize(10).SemiBold().FontColor(Colors.Grey.Darken2);
                                    
                                    mainTable.Cell().PaddingBottom(8).Text(displayValue)
                                        .FontSize(10);
                                }
                            });

                            // Secciones de detalle (maestro-detalle)
                            if (sortedDetailSections != null && sortedDetailSections.Any())
                            {
                                foreach (var detailSection in sortedDetailSections)
                                {
                                    var detailItems = GetDetailItems(item, detailSection.PropertyName);
                                    
                                    if (detailItems != null && detailItems.Any())
                                    {
                                        entityColumn.Item().PaddingTop(15).Column(detailColumn =>
                                        {
                                            // Encabezado de la sección de detalle
                                            detailColumn.Item().PaddingBottom(8).Text(detailSection.DisplayName)
                                                .FontSize(11).Bold().FontColor("#2c5c8a");

                                            // Tabla de detalle
                                            detailColumn.Item().Table(detailTable =>
                                            {
                                                // Definir columnas del detalle
                                                detailTable.ColumnsDefinition(columns =>
                                                {
                                                    foreach (var colDef in detailSection.ColumnDefinitions)
                                                    {
                                                        if (colDef.PropertyName == "Clave" || colDef.PropertyName == "Key")
                                                            columns.ConstantColumn(60);
                                                        else if (colDef.PropertyName == "Valor" || colDef.PropertyName == "Value")
                                                            columns.RelativeColumn();
                                                        else
                                                            columns.RelativeColumn();
                                                    }
                                                });

                                                // Encabezados del detalle
                                                detailTable.Header(header =>
                                                {
                                                    foreach (var colDef in detailSection.ColumnDefinitions)
                                                    {
                                                        header.Cell().Background("#f0f4f8").Padding(5).Text(colDef.DisplayName)
                                                            .FontSize(9).Bold().FontColor("#1a3a5f");
                                                    }
                                                });

                                                // Filas del detalle
                                                foreach (var detailItem in detailItems)
                                                {
                                                    foreach (var colDef in detailSection.ColumnDefinitions)
                                                    {
                                                        var detailValue = GetPropertyValue(detailItem, colDef.PropertyName);
                                                        var displayDetailValue = FormatValue(detailValue, colDef.PropertyName, colDef.ValueFormat);
                                                        
                                                        detailTable.Cell().BorderBottom(1).BorderColor("#e8e8e8")
                                                            .Padding(5).Text(displayDetailValue).FontSize(9);
                                                    }
                                                }
                                            });
                                        });
                                    }
                                }
                            }
                        });

                        column.Item().Height(10);
                    }
                });

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
    private string? GetHighlightValue(T item, string? highlightPropertyName, List<ColumnDefinition> columns)
    {
        if (!string.IsNullOrEmpty(highlightPropertyName))
            return GetPropertyValue(item!, highlightPropertyName)?.ToString();
        
        var highlightColumn = columns.FirstOrDefault(c => c.ShowInHighlight);
        if (highlightColumn != null)
            return GetPropertyValue(item!, highlightColumn.PropertyName)?.ToString();
        
        return columns.FirstOrDefault()?.PropertyName != null ? 
            GetPropertyValue(item!, columns.First().PropertyName)?.ToString() : null;
    }

    private IEnumerable<object>? GetDetailItems(T mainItem, string propertyName)
    {
        var detailProperty = mainItem?.GetType().GetProperty(propertyName, 
            BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
        
        if (detailProperty == null) return null;
        
        var detailValue = detailProperty.GetValue(mainItem);
        return detailValue as IEnumerable<object>;
    }

    private object? GetPropertyValue(object obj, string propertyName)
    {
        if (obj == null) return null;
        
        var property = obj.GetType().GetProperty(propertyName, 
            BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
        
        return property?.GetValue(obj);
    }

    private string FormatValue(object? value, string propertyName, string? format = null)
    {
        if (value == null) return "N/A";
        
        if (value is DateTime date)
            return date.ToString("dd/MM/yyyy HH:mm");
        
        if (value is IFormattable formattable && !string.IsNullOrEmpty(format))
            return formattable.ToString(format, null);
        
        return value.ToString() ?? "N/A";
    }
}
