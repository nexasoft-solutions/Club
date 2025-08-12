namespace NexaSoft.Agro.Domain.Features.Proyectos.EstudiosAmbientales.Dtos;

public sealed record EstudioAmbientalDto
{
    public sealed record EstudioAmbientalDtoResponse(
         Guid Id,
         string Proyecto,
         DateTime FechaInicio,
         DateTime FechaFin,
         string? Detalles,
         DateTime FechaCreacionEstudio,
         CapituloDtoResponse[]? Capitulos
     );

    public sealed record CapituloDtoResponse(
        Guid Id,
        string NombreCapitulo,
        string? DescripcionCapitulo,
        DateTime FechaCreacionCapitulo,
        SubCapituloDtoResponse[]? SubCapitulos
    );

    public sealed record SubCapituloDtoResponse(
        Guid Id,
        string NombreSubCapitulo,
        string? DescripcionSubCapitulo,
        DateTime FechaCreacionSubCapitulo,
        EstructuraDtoResponse[]? Estructuras,
        ArchivoDtoResponse[]? Archivos
    );

    public sealed record EstructuraDtoResponse(
        Guid Id,
        int TipoEstructura,
        string? DescripcionEstructura,
        Guid? PadreEstructuraId,
        Guid SubCapituloId,
        EstructuraDtoResponse[]? Hijos,
        ArchivoDtoResponse[]? Archivos
    );

    public sealed record ArchivoDtoResponse(
        Guid Id,
        string Nombre,
        string? DescripcionArchivo,
        string RutaArchivo,
        DateTime FechaCarga,
        int TipoArchivo,
        DateTime FechaCreacionArchivo,
        PlanoDtoResponse? Plano
    );

    public sealed record PlanoDtoResponse(
        Guid Id,
        decimal Escala,  // Cambiado de string a decimal
        string SistemaProyeccion,
        string NombrePlano,
        string CodigoPlano,
        Guid ColaboradorId,
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
        Guid Id,
        string Proyecto,
        DateOnly FechaInicio,
        DateOnly FechaFin,
        string? Detalles,
        string? codigoEstudio,
        DateTime FechaCreacionEstudio,
        Guid EmpresaId,
        CapitulosResponse[]? Capitulos
        
    );

    public sealed record CapitulosResponse(
        Guid Id,
        string NombreCapitulo,
        string? DescripcionCapitulo,
        DateTime FechaCreacionCapitulo
    );
    
     public sealed record EstudioAmbientalEstructuraResponse(
        Guid Id,
        int TipoEstructura,
        string? NombreEstructura,
        string? DescripcionEstructura,
        Guid? PadreEstructuraId,
        Guid SubCapituloId,
        int EstadoEstructura,
        List<EstudioAmbientalEstructuraResponse?> Hijos
        //ArchivoDtoResponse[]? Archivos
     );
}
