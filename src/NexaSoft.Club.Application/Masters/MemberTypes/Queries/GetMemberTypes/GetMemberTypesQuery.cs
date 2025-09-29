using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.Masters.MemberTypes;

namespace NexaSoft.Club.Application.Masters.MemberTypes.Queries.GetMemberTypes;

public sealed record GetMemberTypesQuery(
    BaseSpecParams SpecParams
) : IQuery<Pagination<MemberTypeResponse>>;
