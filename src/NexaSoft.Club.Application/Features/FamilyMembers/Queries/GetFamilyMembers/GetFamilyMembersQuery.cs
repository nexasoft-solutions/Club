using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.Features.FamilyMembers;

namespace NexaSoft.Club.Application.Features.FamilyMembers.Queries.GetFamilyMembers;

public sealed record GetFamilyMembersQuery(
    BaseSpecParams SpecParams
) : IQuery<Pagination<FamilyMemberResponse>>;
