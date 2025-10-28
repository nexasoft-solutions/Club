using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.HumanResources.CostCenterTypes.Commands.DeleteCostCenterType;

public sealed record DeleteCostCenterTypeCommand(
    long Id,
    string DeletedBy
) : ICommand<bool>;
