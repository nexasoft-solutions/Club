using NexaSoft.Agro.Application.Abstractions.Messaging;

namespace NexaSoft.Agro.Application.Masters.Constantes.Commands.DeleteConstante;

public sealed record DeleteConstanteCommand(
    long Id,
    string UsuarioEliminacion
) : ICommand<bool>;
