using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Masters.FeeConfigurations;
using NexaSoft.Club.Domain.Specifications;

namespace NexaSoft.Club.Application.Masters.FeeConfigurations.Queries.GetMemberTypeFee;

public sealed record GetMemberTypeFeeQuery
(
    BaseSpecParams SpecParams
):IQuery<List<MemberTypeFeeResponse>>;