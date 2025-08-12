namespace NexaSoft.Agro.Api.Controllers.Features.Proyectos.Planos.Request;

public sealed record UpdatePlanoRequest(
    Guid Id,
    int EscalaId,
    string? SistemaProyeccion,
    string? NombrePlano,
    string? CodigoPlano,
    Guid ArchivoId,
    Guid ColaboradorId,
    List<UpdatePlanoRequest> Detalles
);
