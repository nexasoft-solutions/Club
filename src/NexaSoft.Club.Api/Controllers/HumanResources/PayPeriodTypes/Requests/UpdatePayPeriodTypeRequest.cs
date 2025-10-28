namespace NexaSoft.Club.Api.Controllers.HumanResources.PayPeriodTypes.Request;

public sealed record UpdatePayPeriodTypeRequest(
   long Id,
    string? Code,
    string? Name,
    int? Days,
    string? Description,
    string UpdatedBy
);
