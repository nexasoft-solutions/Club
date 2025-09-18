using NexaSoft.Agro.Application.Abstractions.Messaging;

namespace NexaSoft.Agro.Application.Masters.Constantes.Commands.UpdateConstante;

public sealed record UpdateConstanteCommand(
    long Id,
    string? TipoConstante,
    string? Valor,
    string? UsuarioModificacion
) : ICommand<bool>;
