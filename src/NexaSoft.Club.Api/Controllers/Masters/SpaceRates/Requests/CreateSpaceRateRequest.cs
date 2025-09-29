namespace NexaSoft.Club.Api.Controllers.Masters.SpaceRates.Request;

public sealed record CreateSpaceRateRequest(
    long SpaceId,
    long MemberTypeId,
    decimal Rate,
    bool IsActive,
    string CreatedBy
);
