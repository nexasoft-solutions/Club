using NexaSoft.Agro.Application.Abstractions.Messaging;

namespace NexaSoft.Agro.Application.Features.Proyectos.Estructuras.Commands.DeleteEstructura;

public sealed record DeleteEstructuraCommand(
    Guid Id
) : ICommand<bool>;
