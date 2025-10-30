namespace NexaSoft.Club.Api.Controllers.HumanResources.IncomeTaxScales.Request;

public sealed record UpdateIncomeTaxScaleRequest(
   long Id,
    int ScaleYear,
    decimal MinIncome,
    decimal? MaxIncome,
    decimal FixedAmount,
    decimal Rate,
    decimal ExcessOver,
    string? Description,
    string UpdatedBy
);
