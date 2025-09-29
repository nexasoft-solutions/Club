using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.Features.Members;

namespace NexaSoft.Club.Application.Features.Members.Queries.GetMembers;

public sealed record GetMembersQuery(
    BaseSpecParams SpecParams
) : IQuery<Pagination<MemberResponse>>;
