namespace NexaSoft.Club.Api.Controllers.HumanResources.CostCenterTypes.Request;

public sealed record CreateCostCenterTypeRequest(
    string? Code,
    string? Name,
    string? Description,
    string CreatedBy
);
