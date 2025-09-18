using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
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
    public long? SubCapituloId { get; private set; }
    public long? EstructuraId { get; private set; }

    public string? NombreCorto { get; private set; }

    public SubCapitulo? SubCapitulo { get; private set; }
    public Estructura? Estructura { get; private set; }

    //public Plano? Plano { get; private set; }

    public int EstadoArchivo { get; private set; }

    private Archivo() { }

    private Archivo(
        long id,
        string? nombreArchivo,
        string? descripcionArchivo,
        string? rutaArchivo,
        DateOnly fechaCarga,
        int tipoArchivoId,
        long? subCapituloId,
        long? estructuraId,
        string nombreCorto,
        int estadoArchivo,
        DateTime fechaCreacion,
        string? usuarioCreacion,
        string? usuarioModificacion = null,
        string? usuarioEliminacion = null
    ) : base(fechaCreacion, usuarioCreacion, usuarioModificacion, usuarioEliminacion)
    {
        NombreArchivo = nombreArchivo;
        DescripcionArchivo = descripcionArchivo;
        RutaArchivo = rutaArchivo;
        FechaCarga = fechaCarga;
        TipoArchivoId = tipoArchivoId;
        SubCapituloId = subCapituloId;
        EstructuraId = estructuraId;
        NombreCorto = nombreCorto;
        EstadoArchivo = estadoArchivo;
        FechaCreacion = fechaCreacion;
        UsuarioCreacion = usuarioCreacion;
        UsuarioModificacion = usuarioModificacion;
        UsuarioEliminacion = usuarioEliminacion;
    }

    public static Archivo Create(
       string? nombreArchivo,
       string? descripcionArchivo,
       string? rutaArchivo,
       DateOnly fechaCarga,
       int tipoArchivoId,
       long? subCapituloId,
       long? estructuraId,
       string? nombreCorto,
       int estadoArchivo,
       DateTime fechaCreacion,
       string? usuarioCreacion
    )
    {
        if ((subCapituloId is null && estructuraId is null) ||
        (subCapituloId is not null && estructuraId is not null))
        {
            throw new InvalidOperationException("El archivo debe pertenecer solo a un subcapítulo o una estructura, no a ambos.");
        }

        var entity = new Archivo
        {
            NombreArchivo = nombreArchivo,
            DescripcionArchivo = descripcionArchivo,
            RutaArchivo = rutaArchivo,
            FechaCarga = fechaCarga,
            TipoArchivoId = tipoArchivoId,
            SubCapituloId = subCapituloId,
            EstructuraId = estructuraId,
            NombreCorto = nombreCorto,
            EstadoArchivo = estadoArchivo,
            FechaCreacion = fechaCreacion,
            UsuarioCreacion = usuarioCreacion
        };

        //entity.RaiseDomainEvent(new ArchivoCreateDomainEvent(entity.Id));
        return entity;
    }

    public Result Update(
       long Id,
       string? descripcionArchivo,
       string? nombreCorto,
       DateTime utcNow,
       string? usuarioModificacion
    )
    {
        DescripcionArchivo = descripcionArchivo;
        NombreCorto = nombreCorto;
        FechaModificacion = utcNow;
        UsuarioModificacion = usuarioModificacion;
        //RaiseDomainEvent(new ArchivoUpdateDomainEvent(this.Id));
        return Result.Success();
    }


    public Result Delete(DateTime utcNow, string usuarioEliminacion)
    {
        EstadoArchivo = (int)EstadosEnum.Eliminado;
        FechaEliminacion = utcNow;
        UsuarioEliminacion = UsuarioEliminacion;
        return Result.Success();
    }
}
