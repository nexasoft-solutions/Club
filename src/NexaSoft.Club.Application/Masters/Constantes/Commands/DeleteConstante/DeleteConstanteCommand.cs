using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.Masters.Constantes.Commands.DeleteConstante;

public sealed record DeleteConstanteCommand(
    long Id,
    string UsuarioEliminacion
) : ICommand<bool>;
