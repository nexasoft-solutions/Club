using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.Masters.Constantes.Commands.CreateConstante;

public sealed record CreateConstanteCommand(
    string? TipoConstante,
    string? Valor,
    string? CreatedBy
) : ICommand<long>;
