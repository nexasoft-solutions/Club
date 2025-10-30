namespace NexaSoft.Club.Api.Controllers.HumanResources.CostCenters.Request;

public sealed record CreateCostCenterRequest(
    string? Code,
    string? Name,
    long? ParentCostCenterId,
    long? CostCenterTypeId,
    string? Description,
    long? ResponsibleId,
    decimal Budget,
    DateOnly StartDate,
    DateOnly? EndDate,
    string CreatedBy
);
