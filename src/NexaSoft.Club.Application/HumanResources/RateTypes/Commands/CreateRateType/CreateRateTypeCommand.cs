using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.HumanResources.RateTypes.Commands.CreateRateType;

public sealed record CreateRateTypeCommand(
    string? Code,
    string? Name,
    string? Description,
    string CreatedBy
) : ICommand<long>;
