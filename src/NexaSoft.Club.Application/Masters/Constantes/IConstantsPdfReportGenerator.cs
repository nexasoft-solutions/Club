using NexaSoft.Club.Application.Masters.Constantes.Queries.GetConstesMultiple;

namespace NexaSoft.Club.Application.Masters.Constantes;

public interface IConstantsPdfReportGenerator
{
    byte[] GenerateConstantsReport(
           IEnumerable<ConstantesAgrupadasResponse> constantes,
           string title = "Reporte de Constantes",
           string subtitle = "",
           string logoPath = "");
}
