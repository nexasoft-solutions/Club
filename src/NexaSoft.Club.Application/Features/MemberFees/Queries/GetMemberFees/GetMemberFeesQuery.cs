using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.Features.MemberFees;

namespace NexaSoft.Club.Application.Features.MemberFees.Queries.GetMemberFees;

public sealed record GetMemberFeesQuery(
    BaseSpecParams SpecParams
) : IQuery<Pagination<MemberFeeResponse>>;
