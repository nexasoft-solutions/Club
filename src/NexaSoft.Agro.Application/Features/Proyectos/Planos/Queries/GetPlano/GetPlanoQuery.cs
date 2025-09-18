using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Domain.Features.Proyectos.Planos;

namespace NexaSoft.Agro.Application.Features.Proyectos.Planos.Queries.GetPlano;

public sealed record GetPlanoQuery(
    long Id
) : IQuery<PlanoResponse>;
