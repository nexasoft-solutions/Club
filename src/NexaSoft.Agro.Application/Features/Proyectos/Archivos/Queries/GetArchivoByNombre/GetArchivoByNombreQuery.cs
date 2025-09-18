using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Domain.Features.Proyectos.Archivos;

namespace NexaSoft.Agro.Application.Features.Proyectos.Archivos.Queries.GetArchivoByNombre;

public sealed record GetArchivoByNombreQuery(
    long EstudioAmbientalId,
    string Descripcion
): IQuery<List<ArchivoItemResponse>>;
