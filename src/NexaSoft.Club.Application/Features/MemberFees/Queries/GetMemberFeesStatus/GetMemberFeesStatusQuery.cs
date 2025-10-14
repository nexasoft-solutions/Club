using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Features.MemberFees;

namespace NexaSoft.Club.Application.Features.MemberFees.Queries.GetMemberFeesStatus;

public record class GetMemberFeesStatusQuery
(
    long MemberId,
    long StatusId,
    string OrderBy = "DueDate"
): IQuery<List<MemberFeeResponse>>;
