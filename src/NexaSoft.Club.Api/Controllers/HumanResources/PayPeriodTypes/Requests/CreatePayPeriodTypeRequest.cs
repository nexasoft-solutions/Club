namespace NexaSoft.Club.Api.Controllers.HumanResources.PayPeriodTypes.Request;

public sealed record CreatePayPeriodTypeRequest(
    string? Code,
    string? Name,
    int? Days,
    string? Description,
    string CreatedBy
);
