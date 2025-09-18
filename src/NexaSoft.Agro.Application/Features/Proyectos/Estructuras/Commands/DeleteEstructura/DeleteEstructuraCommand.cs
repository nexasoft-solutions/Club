using NexaSoft.Agro.Application.Abstractions.Messaging;

namespace NexaSoft.Agro.Application.Features.Proyectos.Estructuras.Commands.DeleteEstructura;

public sealed record DeleteEstructuraCommand(
    long Id,
    string UsuarioEliminacion
) : ICommand<bool>;
