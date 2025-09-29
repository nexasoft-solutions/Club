using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Features.MemberFees;

namespace NexaSoft.Club.Application.Features.MemberFees.Queries.GetMemberFee;

public sealed record GetMemberFeeQuery(
    long Id
) : IQuery<MemberFeeResponse>;
