namespace NexaSoft.Agro.Api.Controllers.Masters.Consultoras.Request;

public sealed record CreateConsultoraRequest(
    string? NombreConsultora,
    string? DireccionConsultora,
    string? RepresentanteConsultora,
    string? RucConsultora,
    string? CorreoOrganizacional
);
