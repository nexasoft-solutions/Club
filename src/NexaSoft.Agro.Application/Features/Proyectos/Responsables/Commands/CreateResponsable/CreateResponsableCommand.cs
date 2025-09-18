using NexaSoft.Agro.Application.Abstractions.Messaging;

namespace NexaSoft.Agro.Application.Features.Proyectos.Responsables.Commands.CreateResponsable;

public sealed record CreateResponsableCommand(
    string? NombreResponsable,
    string? CargoResponsable,
    string? CorreoResponsable,
    string? TelefonoResponsable,
    string? Observaciones,
    long? EstudioAmbientalId,
    string? UsuarioCreacion
) : ICommand<long>;
