namespace NexaSoft.Club.Api.Controllers.Masters.SpaceRates.Requests;

public sealed record GetSpaceRateByMemberTypeRequest
(
    long SpaceId,
    long MemberTypeId    
);