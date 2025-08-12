using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Domain.Features.Proyectos.Planos;

namespace NexaSoft.Agro.Application.Features.Proyectos.Planos.Queries.GetPlanoByArchivo;

public sealed record GetPlanoByArchivoQuery(Guid ArchivoId):IQuery<PlanoItemResponse>;

