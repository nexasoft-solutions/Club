using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.HumanResources.CostCenters.Commands.CreateCostCenter;

public sealed record CreateCostCenterCommand(
    string? Code,
    string? Name,
    long? ParentCostCenterId,
    long? CostCenterTypeId,
    string? Description,
    long? ResponsibleId,
    decimal Budget,
    DateOnly StartDate,
    DateOnly EndDate,
    string CreatedBy
) : ICommand<long>;
