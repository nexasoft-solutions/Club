using System.Runtime.InteropServices;
using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.Masters.Ubigeos.Commands.CreateUbigeo;

public sealed record CreateUbigeoCommand(
    string? Description,
    int Level,
    long? ParentId,
    string? CreatedBy
) : ICommand<long>;
