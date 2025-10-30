namespace NexaSoft.Club.Domain.HumanResources.CostCenters;

public sealed record CostCenterResponse(
    long Id,
    string? Code,
    string? Name,
    long? ParentCostCenterId,
    string? CostCenterName,
    long? CostCenterTypeId,
    string? CostCenterTypeName,
    string? Description,
    long? ResponsibleId,
    string? EmployeeCode,
    decimal Budget,
    DateOnly StartDate,
    DateOnly? EndDate,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    string? CreatedBy,
    string? UpdatedBy
);
