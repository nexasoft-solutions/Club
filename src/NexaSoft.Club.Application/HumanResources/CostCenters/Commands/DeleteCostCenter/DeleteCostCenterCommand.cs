using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.HumanResources.CostCenters.Commands.DeleteCostCenter;

public sealed record DeleteCostCenterCommand(
    long Id,
    string DeletedBy
) : ICommand<bool>;
