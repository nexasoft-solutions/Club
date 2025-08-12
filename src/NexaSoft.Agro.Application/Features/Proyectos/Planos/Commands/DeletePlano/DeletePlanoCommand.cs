using NexaSoft.Agro.Application.Abstractions.Messaging;

namespace NexaSoft.Agro.Application.Features.Proyectos.Planos.Commands.DeletePlano;

public sealed record DeletePlanoCommand(
    Guid Id
) : ICommand<bool>;
