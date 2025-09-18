using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Domain.Features.Proyectos.EstudiosAmbientales;

namespace NexaSoft.Agro.Application.Features.Proyectos.EstudiosAmbientales.Queries.GetEstudiosReport;

public class GetEstudioReportQueryHandler
(
    IEstudioAmbientalRepository _repository,
    IStudyTreePdfReportGenerator _pdfGenerator
) : IQueryHandler<GetEstudioReportQuery, byte[]>
{
    public async Task<Result<byte[]>> Handle(GetEstudioReportQuery request, CancellationToken cancellationToken)
    {
        var data = await _repository.GetEstudioAmbientalByIdAsync(request.Id, cancellationToken);
        if (data == null)
            return Result.Failure<byte[]>(EstudioAmbientalErrores.NoHayConincidencias);


        var pdfBytes = _pdfGenerator.GenerateStudyTreeReport(
            data,
            title: "Reporte de Estudio Ambiental",
            subtitle: $"Generado el {DateTime.Now:dd/MM/yyyy HH:mm}",
            logoPath: "" // Agrega tu path del logo si es necesario
        );

        return Result.Success(pdfBytes);

    }
}
