using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Domain.Masters.Constantes;

namespace NexaSoft.Agro.Application.Masters.Constantes.Queries.GetConstante;

public sealed record GetConstanteQuery(
    long Id
) : IQuery<ConstanteResponse>;
