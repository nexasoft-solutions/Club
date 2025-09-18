namespace NexaSoft.Agro.Domain.Masters.Consultoras;

public sealed record ConsultoraResponse(
    long Id,
    string? NombreConsultora,
    string? DireccionConsultora,
    string? RepresentanteConsultora,
    string? RucConsultora,
    string? CorreoOrganizacional,
    DateTime FechaCreacion,
    DateTime? FechaModificacion,
    string? UsuarioCreacion,
    string? UsuarioModificacion
);
