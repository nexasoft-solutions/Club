using NexaSoft.Agro.Application.Abstractions.Messaging;

namespace NexaSoft.Agro.Application.Masters.Ubigeos.Commands.DeleteUbigeo;

public sealed record DeleteUbigeoCommand(
    long Id,
    string UsuarioEliminacion
) : ICommand<bool>;
