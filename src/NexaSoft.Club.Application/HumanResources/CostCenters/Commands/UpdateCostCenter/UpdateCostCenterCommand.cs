using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.HumanResources.CostCenters.Commands.UpdateCostCenter;

public sealed record UpdateCostCenterCommand(
    long Id,
    string? Code,
    string? Name,
    long? ParentCostCenterId,
    long? CostCenterTypeId,
    string? Description,
    long? ResponsibleId,
    decimal Budget,
    DateOnly StartDate,
    DateOnly? EndDate,
    string UpdatedBy
) : ICommand<bool>;
