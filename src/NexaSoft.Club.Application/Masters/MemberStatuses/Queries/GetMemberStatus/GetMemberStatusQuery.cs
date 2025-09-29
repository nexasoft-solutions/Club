using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Masters.MemberStatuses;

namespace NexaSoft.Club.Application.Masters.MemberStatuses.Queries.GetMemberStatus;

public sealed record GetMemberStatusQuery(
    long Id
) : IQuery<MemberStatusResponse>;
