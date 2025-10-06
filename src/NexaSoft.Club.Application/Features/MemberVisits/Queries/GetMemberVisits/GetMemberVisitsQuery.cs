using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.Features.MemberVisits;

namespace NexaSoft.Club.Application.Features.MemberVisits.Queries.GetMemberVisits;

public sealed record GetMemberVisitsQuery(
    BaseSpecParams SpecParams
) : IQuery<Pagination<MemberVisitResponse>>;
