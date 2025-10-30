namespace NexaSoft.Club.Domain.HumanResources.IncomeTaxScales;

public sealed record IncomeTaxScaleResponse(
    long Id,
    int ScaleYear,
    decimal MinIncome,
    decimal? MaxIncome,
    decimal FixedAmount,
    decimal Rate,
    decimal ExcessOver,
    string? Description,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    string? CreatedBy,
    string? UpdatedBy
);
