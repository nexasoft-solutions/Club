using NexaSoft.Agro.Domain.Abstractions;

namespace NexaSoft.Agro.Application.Abstractions.Reporting;

public interface IPdfReportGenerator<T>
{
   byte[] Generate(
        IEnumerable<T> items,
        List<ColumnDefinition> columnDefinitions,
        string title = "Reporte",
        string subtitle = "",
        string logoPath = "",
        string entityName = "Registro",
        string? idPropertyName = "Id",
        string? highlightPropertyName = null,
        List<DetailSectionDefinition>? detailSections = null
    );
}
