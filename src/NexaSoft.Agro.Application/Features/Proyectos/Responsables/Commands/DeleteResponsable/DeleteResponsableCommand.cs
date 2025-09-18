using NexaSoft.Agro.Application.Abstractions.Messaging;

namespace NexaSoft.Agro.Application.Features.Proyectos.Responsables.Commands.DeleteResponsable;

public sealed record DeleteResponsableCommand(
    long Id,
    string UsuarioEliminacion
) : ICommand<bool>;
