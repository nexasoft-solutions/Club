using NexaSoft.Agro.Application.Abstractions.Messaging;

namespace NexaSoft.Agro.Application.Masters.Constantes.Commands.CreateConstante;

public sealed record CreateConstanteCommand(
    string? TipoConstante,
    string? Valor
) : ICommand<Guid>;
