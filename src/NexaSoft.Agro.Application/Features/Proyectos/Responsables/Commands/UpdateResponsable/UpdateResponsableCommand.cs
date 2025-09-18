using NexaSoft.Agro.Application.Abstractions.Messaging;

namespace NexaSoft.Agro.Application.Features.Proyectos.Responsables.Commands.UpdateResponsable;

public sealed record UpdateResponsableCommand(
    long Id,
    string? NombreResponsable,
    string? CargoResponsable,
    string? CorreoResponsable,
    string? TelefonoResponsable,
    string? Observaciones,
    string? UsuarioModificacion
) : ICommand<bool>;
