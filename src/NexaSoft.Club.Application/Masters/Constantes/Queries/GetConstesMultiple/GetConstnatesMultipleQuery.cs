using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.Masters.Constantes.Queries.GetConstesMultiple;

public sealed record GetConstnatesMultipleQuery
(
    List<string> TiposConstante
): IQuery<IReadOnlyList<ConstantesAgrupadasResponse>>;
