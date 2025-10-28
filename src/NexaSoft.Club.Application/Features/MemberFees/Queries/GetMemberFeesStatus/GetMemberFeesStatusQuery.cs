using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Features.MemberFees;

namespace NexaSoft.Club.Application.Features.MemberFees.Queries.GetMemberFeesStatus;

public record class GetMemberFeesStatusQuery
(
    long MemberId,
    IEnumerable<long> StatusIds,
    string OrderBy = "DueDate"
): IQuery<List<MemberFeeResponse>>;
