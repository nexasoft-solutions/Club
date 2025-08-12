using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Domain.Features.Proyectos.Archivos;

namespace NexaSoft.Agro.Application.Features.Proyectos.Archivos.Queries.GetArchivo;

public sealed record GetArchivoQuery(
    Guid Id
) : IQuery<ArchivoResponse>;
