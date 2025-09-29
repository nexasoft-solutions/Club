using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.Masters.Constantes.Commands.CreateConstantes;

public sealed record CreateConstantesBatchCommand
(
    List<CreateConstantesCommand> Constantes
) : ICommand<int>;

