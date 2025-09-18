using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Domain.Features.Proyectos.Estructuras;

namespace NexaSoft.Agro.Application.Features.Proyectos.Estructuras.Queries.GetEstructura;

public sealed record GetEstructuraQuery(
    long Id
) : IQuery<EstructuraResponse>;
