namespace NexaSoft.Club.Api.Controllers.HumanResources.CostCenterTypes.Request;

public sealed record UpdateCostCenterTypeRequest(
   long Id,
    string? Code,
    string? Name,
    string? Description,
    string UpdatedBy
);
