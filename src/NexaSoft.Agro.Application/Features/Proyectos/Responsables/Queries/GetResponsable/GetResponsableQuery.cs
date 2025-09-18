using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Domain.Features.Proyectos.Responsables;

namespace NexaSoft.Agro.Application.Features.Proyectos.Responsables.Queries.GetResponsable;

public sealed record GetResponsableQuery(
    long Id
) : IQuery<ResponsableResponse>;
