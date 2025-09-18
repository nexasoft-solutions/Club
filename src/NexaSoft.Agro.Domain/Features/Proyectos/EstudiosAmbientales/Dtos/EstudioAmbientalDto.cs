namespace NexaSoft.Agro.Domain.Features.Proyectos.EstudiosAmbientales.Dtos;

public sealed record EstudioAmbientalDto
{
    public sealed record EstudioAmbientalDtoResponse(
         long Id,
         string Proyecto,
         DateTime FechaInicio,
         DateTime FechaFin,
         string? Detalles,
         DateTime FechaCreacionEstudio,
         CapituloDtoResponse[]? Capitulos
     );

    public sealed record CapituloDtoResponse(
        long Id,
        string NombreCapitulo,
        string? DescripcionCapitulo,
        DateTime FechaCreacionCapitulo,
        SubCapituloDtoResponse[]? SubCapitulos
    );

    public sealed record SubCapituloDtoResponse(
        long Id,
        string NombreSubCapitulo,
        string? DescripcionSubCapitulo,
        DateTime FechaCreacionSubCapitulo,
        EstructuraDtoResponse[]? Estructuras,
        ArchivoDtoResponse[]? Archivos
    );

    public sealed record EstructuraDtoResponse(
        long Id,
        int TipoEstructura,
        string? NombreEstructura,
        string? DescripcionEstructura,
        long? PadreEstructuraId,
        long SubCapituloId,
        EstructuraDtoResponse[]? Hijos,
        ArchivoDtoResponse[]? Archivos
    );

    public sealed record ArchivoDtoResponse(
        long Id,
        string Nombre,
        string? DescripcionArchivo,
        string RutaArchivo,
        DateTime FechaCarga,
        int TipoArchivo,
        DateTime FechaCreacionArchivo,
        PlanoDtoResponse? Plano
    );

    public sealed record PlanoDtoResponse(
        long Id,
        decimal Escala, 
        string SistemaProyeccion,
        string NombrePlano,
        string CodigoPlano,
        long ColaboradorId,
        DateTime FechaCreacionPlano,
        PlanoDetalleDtoResponse[]? Detalles = null  // Valor por defecto
    );

    public sealed record PlanoDetalleDtoResponse(
        string DescripcionPlano,
        double Latitud,
        double Longitud,
        decimal Area
    );

    public sealed record EstudioAmbientalCapituloResponse(
        long Id,
        string Proyecto,
        DateOnly FechaInicio,
        DateOnly FechaFin,
        string? Detalles,
        string? codigoEstudio,
        DateTime FechaCreacionEstudio,
        long EmpresaId,
        CapitulosResponse[]? Capitulos
        
    );

    public sealed record CapitulosResponse(
        long Id,
        string NombreCapitulo,
        string? DescripcionCapitulo,
        DateTime FechaCreacionCapitulo
    );
    
     public sealed record EstudioAmbientalEstructuraResponse(
        long Id,
        int TipoEstructura,
        string? NombreEstructura,
        string? DescripcionEstructura,
        long? PadreEstructuraId,
        long SubCapituloId,
        int EstadoEstructura,
        List<EstudioAmbientalEstructuraResponse?> Hijos
        //ArchivoDtoResponse[]? Archivos
     );
}
