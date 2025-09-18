using NexaSoft.Agro.Application.Abstractions.Messaging;

namespace NexaSoft.Agro.Application.Masters.Constantes.Queries.GetConstesMultiple;

public sealed record GetConstnatesMultipleQuery
(
    List<string> TiposConstante
): IQuery<IReadOnlyList<ConstantesAgrupadasResponse>>;
