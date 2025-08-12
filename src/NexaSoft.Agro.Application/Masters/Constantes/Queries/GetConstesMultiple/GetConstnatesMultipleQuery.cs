using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Domain.Masters.Constantes;

namespace NexaSoft.Agro.Application.Masters.Constantes.Queries.GetConstesMultiple;

public sealed record GetConstnatesMultipleQuery
(
    List<string> TiposConstante
): IQuery<IReadOnlyList<ConstamtesAgrupadasResponse>>;
