using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.Masters.Constantes.Commands.UpdateConstante;

public sealed record UpdateConstanteCommand(
    long Id,
    string? TipoConstante,
    string? Valor,
    string? UsuarioModificacion
) : ICommand<bool>;
