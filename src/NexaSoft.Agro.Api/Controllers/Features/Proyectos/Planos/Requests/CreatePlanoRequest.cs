using NexaSoft.Agro.Api.Controllers.Features.Proyectos.Planos.Requests;

namespace NexaSoft.Agro.Api.Controllers.Features.Proyectos.Planos.Request;

public sealed record CreatePlanoRequest(
    int EscalaId,
    string? SistemaProyeccion,
    string? NombrePlano,
    string? CodigoPlano,
    Guid ArchivoId,
    Guid ColaboradorId,
    List<CreatePlanoDetalleRequest> Detalles
);



