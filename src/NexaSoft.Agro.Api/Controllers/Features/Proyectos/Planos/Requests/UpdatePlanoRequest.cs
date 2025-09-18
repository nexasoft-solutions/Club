namespace NexaSoft.Agro.Api.Controllers.Features.Proyectos.Planos.Request;

public sealed record UpdatePlanoRequest(
    long Id,
    int EscalaId,
    string? SistemaProyeccion,
    string? NombrePlano,
    string? CodigoPlano,
    long ArchivoId,
    long ColaboradorId,
    List<UpdatePlanoRequest> Detalles
);
