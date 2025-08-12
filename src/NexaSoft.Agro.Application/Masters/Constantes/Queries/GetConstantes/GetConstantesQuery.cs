using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Domain.Specifications;
using NexaSoft.Agro.Application.Abstractions.RequestHelpers;
using NexaSoft.Agro.Domain.Masters.Constantes;

namespace NexaSoft.Agro.Application.Masters.Constantes.Queries.GetConstantes;

public sealed record GetConstantesQuery(
    BaseSpecParams SpecParams
) : IQuery<Pagination<ConstanteResponse>>;
