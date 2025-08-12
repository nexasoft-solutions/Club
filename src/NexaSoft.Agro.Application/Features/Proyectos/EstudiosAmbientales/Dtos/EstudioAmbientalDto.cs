
namespace NexaSoft.Agro.Application.Features.Proyectos.EstudiosAmbientales.Dtos;

public sealed record EstudioAmbientalDetalleResponse(
    Guid Id,
    string? Proyecto,
    DateOnly FechaInicio,
    DateOnly FechaFin,
    string? Detalles,
    Guid EmpresaId,
    List<CapituloResponseDto> Capitulos
    //List<ArchivoResponseDto> Archivos // Archivos del estudio ambiental
);

public sealed record CapituloResponseDto(
    Guid Id,
    string Nombre,
    string Descripcion,
    Guid EstudioAmbientalId,
    List<SubCapituloResponseDto> SubCapitulos
    //List<ArchivoResponseDto> Archivos // Archivos del capítulo
);

public sealed record SubCapituloResponseDto(
    Guid Id,
    string Nombre,
    string Descripcion,
    Guid CapituloId,
    List<EstructuraResponseDto> Estructuras,
    List<ArchivoResponseDto> Archivos // Archivos del capítulo
);

public sealed record EstructuraResponseDto(
    Guid Id,
    int Tipo,
    string Descripcion,
    Guid? PadreEstructuraId,
    List<EstructuraResponseDto> Hijos,
    List<ArchivoResponseDto> Archivos // Archivos del capítulo
);

public sealed record ArchivoResponseDto(
    Guid Id,
    string? NombreArchivo,
    string? DescripcionArchivo,
    string? RutaArchivo,
    DateOnly FechaCarga,
    int TipoArchivo,
    PlanoResponseDto? Plano = null
);


public record PlanoResponseDto(
    Guid Id,
    int Escala,
    string? SistemaProyeccion,
    string? NombrePlano,
    string? CodigoPlano,
    Guid ArchivoId,
    Guid ColaboradorId,
    List<PlanoDetalleResponseDto> Detalles
);

public record PlanoDetalleResponseDto(
    Guid Id,
    string? Descripcion,
    double Latitud,
    double Longitud,
    double Area
);