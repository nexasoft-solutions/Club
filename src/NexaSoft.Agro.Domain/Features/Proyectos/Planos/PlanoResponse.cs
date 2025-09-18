using NetTopologySuite.Geometries;

namespace NexaSoft.Agro.Domain.Features.Proyectos.Planos;

public sealed record PlanoResponse(
    long Id,
    string? Escala,
    string? SistemaProyeccion,
    string? NombrePlano,
    string? CodigoPlano,
    string? NombreArchivo,
    string? UserName,
    long ArchivoId,
    List<PlanoDetalleResponse> Detalles,
    DateTime FechaCreacion,
    string? UsuarioCreacion
);

public sealed record PlanoDetalleResponse(
    long Id,
    string? Descripcion,
    Geometry Coordenadas
);

public sealed record PlanoItemResponse(
    long Id,
    string? Escala,
    string? SistemaProyeccion,
    string? NombrePlano,
    string? CodigoPlano,
    string? UserName,
    long ArchivoId,
    List<PlanoDetalleResponse> Detalles,
    int EscalaId,
    DateTime FechaCreacion
);