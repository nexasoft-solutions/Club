using NexaSoft.Agro.Application.Masters.Constantes.Queries.GetConstesMultiple;

namespace NexaSoft.Agro.Application.Masters.Constantes;

public interface IConstantsPdfReportGenerator
{
    byte[] GenerateConstantsReport(
           IEnumerable<ConstantesAgrupadasResponse> constantes,
           string title = "Reporte de Constantes",
           string subtitle = "",
           string logoPath = "");
}
