using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.Masters.MemberStatuses;

namespace NexaSoft.Club.Application.Masters.MemberStatuses.Queries.GetMemberStatuses;

public sealed record GetMemberStatusesQuery(
    BaseSpecParams SpecParams
) : IQuery<Pagination<MemberStatusResponse>>;
