using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.Features.Members.Queries.GetMemberMetrics;

public sealed record  GetMemberMetricQuery
(
    long MemberId
) : IQuery<MemberDataResponse>;


public sealed record MemberDataResponse
(
    decimal CurrentBalance,
    int ActiveBookings,
    int TotalVisitsThisMonth,
    int RemainingDaysInMembership
);
