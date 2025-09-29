using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.Masters.Ubigeos.Commands.DeleteUbigeo;

public sealed record DeleteUbigeoCommand(
    long Id,
    string UsuarioEliminacion
) : ICommand<bool>;
