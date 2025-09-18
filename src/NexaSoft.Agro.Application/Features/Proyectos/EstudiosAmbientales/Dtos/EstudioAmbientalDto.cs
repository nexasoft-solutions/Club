
namespace NexaSoft.Agro.Application.Features.Proyectos.EstudiosAmbientales.Dtos;

public sealed record EstudioAmbientalDetalleResponse(
    long Id,
    string? Proyecto,
    DateOnly FechaInicio,
    DateOnly FechaFin,
    string? Detalles,
    long EmpresaId,
    List<CapituloResponseDto> Capitulos
    //List<ArchivoResponseDto> Archivos // Archivos del estudio ambiental
);

public sealed record CapituloResponseDto(
    long Id,
    string Nombre,
    string Descripcion,
    long EstudioAmbientalId,
    List<SubCapituloResponseDto> SubCapitulos
    //List<ArchivoResponseDto> Archivos // Archivos del capítulo
);

public sealed record SubCapituloResponseDto(
    long Id,
    string Nombre,
    string Descripcion,
    long CapituloId,
    List<EstructuraResponseDto> Estructuras,
    List<ArchivoResponseDto> Archivos // Archivos del capítulo
);

public sealed record EstructuraResponseDto(
    long Id,
    int Tipo,
    string Descripcion,
    long? PadreEstructuraId,
    List<EstructuraResponseDto> Hijos,
    List<ArchivoResponseDto> Archivos // Archivos del capítulo
);

public sealed record ArchivoResponseDto(
    long Id,
    string? NombreArchivo,
    string? DescripcionArchivo,
    string? RutaArchivo,
    DateOnly FechaCarga,
    int TipoArchivo,
    PlanoResponseDto? Plano = null
);


public record PlanoResponseDto(
    long Id,
    int Escala,
    string? SistemaProyeccion,
    string? NombrePlano,
    string? CodigoPlano,
    long ArchivoId,
    long ColaboradorId,
    List<PlanoDetalleResponseDto> Detalles
);

public record PlanoDetalleResponseDto(
    long Id,
    string? Descripcion,
    double Latitud,
    double Longitud,
    double Area
);