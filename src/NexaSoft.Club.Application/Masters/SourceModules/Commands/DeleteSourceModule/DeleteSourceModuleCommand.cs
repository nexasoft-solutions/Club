using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.Masters.SourceModules.Commands.DeleteSourceModule;

public sealed record DeleteSourceModuleCommand(
    long Id,
    string DeletedBy
) : ICommand<bool>;
