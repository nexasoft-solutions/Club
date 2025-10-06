using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Features.MemberVisits;

namespace NexaSoft.Club.Application.Features.MemberVisits.Queries.GetMemberVisit;

public sealed record GetMemberVisitQuery(
    long Id
) : IQuery<MemberVisitResponse>;
