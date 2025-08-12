using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Domain.Features.Proyectos.Archivos;

namespace NexaSoft.Agro.Application.Features.Proyectos.Archivos.Queries.GetArchivoDownload;

public sealed record GetArchivoDownloadQuery(Guid Id): IQuery<string>;
