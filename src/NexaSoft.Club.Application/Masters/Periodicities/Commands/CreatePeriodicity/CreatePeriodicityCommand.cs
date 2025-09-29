using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.Masters.Periodicities.Commands.CreatePeriodicity;

public sealed record CreatePeriodicityCommand(
    string? Name,
    string? Description,
    string CreatedBy
) : ICommand<long>;
