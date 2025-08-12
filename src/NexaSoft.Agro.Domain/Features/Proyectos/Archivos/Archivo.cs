using NexaSoft.Agro.Domain.Abstractions;
using NexaSoft.Agro.Domain.Features.Proyectos.Archivos.Events;
using NexaSoft.Agro.Domain.Features.Proyectos.Estructuras;
using NexaSoft.Agro.Domain.Features.Proyectos.SubCapitulos;
using static NexaSoft.Agro.Domain.Shareds.Enums;

namespace NexaSoft.Agro.Domain.Features.Proyectos.Archivos;

public class Archivo : Entity
{
    public string? NombreArchivo { get; private set; }
    public string? DescripcionArchivo { get; private set; }
    public string? RutaArchivo { get; private set; }
    public DateOnly FechaCarga { get; private set; }
    public int TipoArchivoId { get; private set; }
    public Guid? SubCapituloId { get; private set; }
    public Guid? EstructuraId { get; private set; }

    public SubCapitulo? SubCapitulo { get; private set; }
    public Estructura? Estructura { get; private set; }

    //public Plano? Plano { get; private set; }

    public int EstadoArchivo { get; private set; }

    private Archivo() { }

    private Archivo(
        Guid id,
        string? nombreArchivo,
        string? descripcionArchivo,
        string? rutaArchivo,
        DateOnly fechaCarga,
        int tipoArchivoId,
        Guid? subCapituloId,
        Guid? estructuraId,
        int estadoArchivo,
        DateTime fechaCreacion
    ) : base(id, fechaCreacion)
    {
        NombreArchivo = nombreArchivo;
        DescripcionArchivo = descripcionArchivo;
        RutaArchivo = rutaArchivo;
        FechaCarga = fechaCarga;
        TipoArchivoId = tipoArchivoId;
        SubCapituloId = subCapituloId;
        EstructuraId = estructuraId;
        EstadoArchivo = estadoArchivo;
        FechaCreacion = fechaCreacion;
    }

    public static Archivo Create(
       string? nombreArchivo,
       string? descripcionArchivo,
       string? rutaArchivo,
       DateOnly fechaCarga,
       int tipoArchivoId,
       Guid? subCapituloId,
       Guid? estructuraId,
       int estadoArchivo,
       DateTime fechaCreacion)
    {
        if ((subCapituloId is null && estructuraId is null) ||
        (subCapituloId is not null && estructuraId is not null))
        {
            throw new InvalidOperationException("El archivo debe pertenecer solo a un subcapítulo o una estructura, no a ambos.");
        }

        var entity = new Archivo
        {
            Id = Guid.NewGuid(),
            NombreArchivo = nombreArchivo,
            DescripcionArchivo = descripcionArchivo,
            RutaArchivo = rutaArchivo,
            FechaCarga = fechaCarga,
            TipoArchivoId = tipoArchivoId,
            SubCapituloId = subCapituloId,
            EstructuraId = estructuraId,
            EstadoArchivo = estadoArchivo,
            FechaCreacion = fechaCreacion
        };

        entity.RaiseDomainEvent(new ArchivoCreateDomainEvent(entity.Id));
        return entity;
    }

    public Result Update(
       Guid Id,   
       string? descripcionArchivo,      
       DateTime utcNow)
    {
        DescripcionArchivo = descripcionArchivo;
        FechaModificacion = utcNow;
        RaiseDomainEvent(new ArchivoUpdateDomainEvent(this.Id));
        return Result.Success();
    }


    public Result Delete(DateTime utcNow)
    {
        EstadoArchivo = (int)EstadosEnum.Eliminado;
        FechaEliminacion = utcNow;
        return Result.Success();
    }
}
