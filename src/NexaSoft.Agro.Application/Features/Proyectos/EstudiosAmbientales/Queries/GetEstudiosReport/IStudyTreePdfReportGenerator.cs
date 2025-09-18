using System;
using static NexaSoft.Agro.Domain.Features.Proyectos.EstudiosAmbientales.Dtos.EstudioAmbientalDto;

namespace NexaSoft.Agro.Application.Features.Proyectos.EstudiosAmbientales.Queries.GetEstudiosReport;

public interface IStudyTreePdfReportGenerator
{
    byte[] GenerateStudyTreeReport(
            EstudioAmbientalDtoResponse study,
            string title = "Reporte de Estudio",
            string subtitle = "",
            string logoPath = ""
        );
}
