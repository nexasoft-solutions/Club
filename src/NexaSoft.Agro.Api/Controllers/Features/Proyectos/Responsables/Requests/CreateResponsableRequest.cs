namespace NexaSoft.Agro.Api.Controllers.Features.Proyectos.Responsables.Request;

public sealed record CreateResponsableRequest(
    string? NombreResponsable,
    string? CargoResponsable,
    string? CorreoResponsable,
    string? TelefonoResponsable,
    string? Observaciones,
    long? EstudioAmbientalId,
    string? UsuarioCreacion
);
