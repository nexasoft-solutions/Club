using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.HumanResources.RateTypes.Commands.UpdateRateType;

public sealed record UpdateRateTypeCommand(
    long Id,
    string? Code,
    string? Name,
    string? Description,
    string UpdatedBy
) : ICommand<bool>;
