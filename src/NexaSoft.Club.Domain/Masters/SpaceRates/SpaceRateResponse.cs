namespace NexaSoft.Club.Domain.Masters.SpaceRates;

public sealed record SpaceRateResponse(
    long Id,
    long SpaceId,
    string? SpaceName,
    long MemberTypeId,
    string? TypeName,
    decimal Rate,
    bool IsActive,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    string? CreatedBy,
    string? UpdatedBy
);
