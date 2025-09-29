using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.Masters.Ubigeos;

namespace NexaSoft.Club.Application.Masters.Ubigeos.Queries.GetUbigeos;

public sealed record GetUbigeosQuery(
    BaseSpecParams<long> SpecParams
) : IQuery<Pagination<UbigeoResponse>>;
