using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.Masters.Constantes;

namespace NexaSoft.Club.Application.Masters.Constantes.Queries.GetConstantes;

public sealed record GetConstantesQuery(
    BaseSpecParams SpecParams
) : IQuery<Pagination<ConstanteResponse>>;
