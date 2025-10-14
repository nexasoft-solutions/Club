using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.Masters.SourceModules.Commands.CreateSourceModule;

public sealed record CreateSourceModuleCommand(
    string? Name,
    string? Description,
    string CreatedBy
) : ICommand<long>;
