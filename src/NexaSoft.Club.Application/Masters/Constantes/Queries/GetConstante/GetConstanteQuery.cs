using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Masters.Constantes;

namespace NexaSoft.Club.Application.Masters.Constantes.Queries.GetConstante;

public sealed record GetConstanteQuery(
    long Id
) : IQuery<ConstanteResponse>;
