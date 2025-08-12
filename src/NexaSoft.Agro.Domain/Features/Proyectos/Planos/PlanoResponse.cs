using NetTopologySuite.Geometries;

namespace NexaSoft.Agro.Domain.Features.Proyectos.Planos;

public sealed record PlanoResponse(
    Guid Id,
    string? Escala,
    string? SistemaProyeccion,
    string? NombrePlano,
    string? CodigoPlano,
    string? NombreArchivo,
    string? UserName,
    Guid ArchivoId,
    List<PlanoDetalleResponse> Detalles,
    DateTime FechaCreacion
    //Guid ArchivoId
);

public sealed record PlanoDetalleResponse(
    Guid Id,
    string? Descripcion,
    Geometry Coordenadas
);

public sealed record PlanoItemResponse(
    Guid Id,
    string? Escala,
    string? SistemaProyeccion,
    string? NombrePlano,
    string? CodigoPlano,
    string? UserName,
    Guid ArchivoId,
    List<PlanoDetalleResponse> Detalles,
    int EscalaId,
    DateTime FechaCreacion
);