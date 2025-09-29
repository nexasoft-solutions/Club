using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Features.Members;

namespace NexaSoft.Club.Application.Features.Members.Queries.GetMember;

public sealed record GetMemberQuery(
    long Id
) : IQuery<MemberResponse>;
