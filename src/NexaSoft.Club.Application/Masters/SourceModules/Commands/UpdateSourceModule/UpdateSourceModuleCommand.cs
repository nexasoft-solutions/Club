using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.Masters.SourceModules.Commands.UpdateSourceModule;

public sealed record UpdateSourceModuleCommand(
    long Id,
    string? Name,
    string? Description,
    string UpdatedBy
) : ICommand<bool>;
