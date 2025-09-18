namespace NexaSoft.Agro.Api.Controllers.Masters.Consultoras.Request;

public sealed record UpdateConsultoraRequest(
   long Id,
    string? NombreConsultora,
    string? DireccionConsultora,
    string? RepresentanteConsultora,
    string? RucConsultora,
    string? CorreoOrganizacional,
    string? UsuarioModificacion
);
