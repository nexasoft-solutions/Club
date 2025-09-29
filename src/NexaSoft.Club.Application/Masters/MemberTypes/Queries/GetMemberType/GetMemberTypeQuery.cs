using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Masters.MemberTypes;

namespace NexaSoft.Club.Application.Masters.MemberTypes.Queries.GetMemberType;

public sealed record GetMemberTypeQuery(
    long Id
) : IQuery<MemberTypeResponse>;
