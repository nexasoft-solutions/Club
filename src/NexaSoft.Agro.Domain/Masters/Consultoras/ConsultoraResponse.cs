namespace NexaSoft.Agro.Domain.Masters.Consultoras;

public sealed record ConsultoraResponse(
    Guid Id,
    string? NombreConsultora,
    string? DireccionConsultora,
    string? RepresentanteConsultora,
    string? RucConsultora,
    string? CorreoOrganizacional,
    DateTime FechaCreacion
);
