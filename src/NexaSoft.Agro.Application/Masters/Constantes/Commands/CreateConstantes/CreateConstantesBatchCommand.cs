using NexaSoft.Agro.Application.Abstractions.Messaging;

namespace NexaSoft.Agro.Application.Masters.Constantes.Commands.CreateConstantes;

public sealed record CreateConstantesBatchCommand
(
    List<CreateConstantesCommand> Constantes
) : ICommand<int>;

