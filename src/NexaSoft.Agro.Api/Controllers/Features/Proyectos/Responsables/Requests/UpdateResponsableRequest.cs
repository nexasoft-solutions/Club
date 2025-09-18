namespace NexaSoft.Agro.Api.Controllers.Features.Proyectos.Responsables.Request;

public sealed record UpdateResponsableRequest(
    long Id,
    string? NombreResponsable,
    string? CargoResponsable,
    string? CorreoResponsable,
    string? TelefonoResponsable,
    string? Observaciones,
    string? UsuarioModificacion
);
