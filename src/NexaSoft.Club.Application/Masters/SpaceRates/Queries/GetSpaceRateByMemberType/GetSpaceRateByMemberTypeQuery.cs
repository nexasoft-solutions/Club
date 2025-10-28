using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Masters.SpaceRates;

namespace NexaSoft.Club.Application.Masters.SpaceRates.Queries.GetSpaceRateByMemberType;

public record class GetSpaceRateByMemberTypeQuery(
    long SpaceId,
    long MemberTypeId
) : IQuery<SpaceRateResponse>;
