using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Domain.Specifications;
using NexaSoft.Agro.Application.Abstractions.RequestHelpers;
using NexaSoft.Agro.Domain.Masters.Ubigeos;

namespace NexaSoft.Agro.Application.Masters.Ubigeos.Queries.GetUbigeos;

public sealed record GetUbigeosQuery(
    BaseSpecParams<long> SpecParams
) : IQuery<Pagination<UbigeoResponse>>;
