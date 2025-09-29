namespace NexaSoft.Club.Api.Controllers.Masters.SpaceRates.Request;

public sealed record UpdateSpaceRateRequest(
   long Id,
    long SpaceId,
    long MemberTypeId,
    decimal Rate,
    bool IsActive,
    string UpdatedBy
);
