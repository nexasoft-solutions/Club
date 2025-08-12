using NexaSoft.Agro.Application.Abstractions.Messaging;

namespace NexaSoft.Agro.Application.Masters.Constantes.Commands.UpdateConstante;

public sealed record UpdateConstanteCommand(
    Guid Id,
    string? TipoConstante,
    string? Valor
) : ICommand<bool>;
