namespace NexaSoft.Agro.Domain.Features.Proyectos.Responsables;

public sealed record ResponsableResponse(
    long Id,
    string? NombreResponsable,
    string? CargoResponsable,
    string? CorreoResponsable,
    string? TelefonoResponsable,
    string? Observaciones,
    long? EstudioAmbientalId,
    DateTime FechaCreacion,
    DateTime? FechaModificacion,
    string? UsuarioCreacion,
    string? UsuarioModificacion
);
