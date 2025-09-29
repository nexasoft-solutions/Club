using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Features.FamilyMembers;

namespace NexaSoft.Club.Application.Features.FamilyMembers.Queries.GetFamilyMember;

public sealed record GetFamilyMemberQuery(
    long Id
) : IQuery<FamilyMemberResponse>;
