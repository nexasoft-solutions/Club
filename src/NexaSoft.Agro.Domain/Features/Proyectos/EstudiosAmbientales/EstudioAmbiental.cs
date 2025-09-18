using NexaSoft.Agro.Domain.Abstractions;
using NexaSoft.Agro.Domain.Features.Proyectos.EstudiosAmbientales.Events;
using static NexaSoft.Agro.Domain.Shareds.Enums;
using NexaSoft.Agro.Domain.Features.Organizaciones.Empresas;

namespace NexaSoft.Agro.Domain.Features.Proyectos.EstudiosAmbientales;

public class EstudioAmbiental : Entity
{
    public string? Proyecto { get; private set; }

    public string? CodigoEstudio { get; private set; }
    public DateOnly FechaInicio { get; private set; }
    public DateOnly FechaFin { get; private set; }
    public string? Detalles { get; private set; }
    public long EmpresaId { get; private set; }
    public int EstadoEstudioAmbiental { get; private set; }
    public Empresa? Empresa { get; private set; }


    private EstudioAmbiental() { }

    private EstudioAmbiental(
        string? proyecto,
        string? codigoEstudio,
        DateOnly fechaInicio,
        DateOnly fechaFin,
        string? detalles,
        long empresaId,
        int estadoEstudioAmbiental,
        DateTime fechaCreacion,
        string? usuarioCreacion,
        string? usuarioModificacion = null,
        string? usuarioEliminacion = null
    ) : base(fechaCreacion, usuarioCreacion, usuarioModificacion, usuarioEliminacion)
    {
        Proyecto = proyecto;
        CodigoEstudio = codigoEstudio;
        FechaInicio = fechaInicio;
        FechaFin = fechaFin;
        Detalles = detalles;
        EmpresaId = empresaId;
        EstadoEstudioAmbiental = estadoEstudioAmbiental;
        FechaCreacion = fechaCreacion;
        UsuarioCreacion = usuarioCreacion;
        UsuarioModificacion = usuarioModificacion;
        UsuarioEliminacion = usuarioEliminacion;
    }

    public static EstudioAmbiental Create(
        string? proyecto, 
        string? codigoEstudio,
        DateOnly fechaInicio, 
        DateOnly fechaFin, 
        string? detalles, 
        long empresaId, 
        int estadoEstudioAmbiental, 
        DateTime fechaCreacion,
        string? usuarioCreacion
    )
    {
        var entity = new EstudioAmbiental(
            proyecto,
            codigoEstudio,
            fechaInicio,
            fechaFin,
            detalles,
            empresaId,
            estadoEstudioAmbiental,
            fechaCreacion,
            usuarioCreacion
        );
        //entity.RaiseDomainEvent(new EstudioAmbientalCreateDomainEvent(entity.Id));
        return entity;
    }

    public Result Update(
        long Id,
        string? proyecto, 
        string? codigoEstudio,
        DateOnly fechaInicio, 
        DateOnly fechaFin, 
        string? detalles, 
        long empresaId, 
        DateTime utcNow,
        string? usuarioModificacion
    )
    {
        Proyecto = proyecto;
        CodigoEstudio = codigoEstudio;
        FechaInicio = fechaInicio;
        FechaFin = fechaFin;
        Detalles = detalles;
        EmpresaId = empresaId;
        FechaModificacion = utcNow;
        UsuarioModificacion = usuarioModificacion;

        //RaiseDomainEvent(new EstudioAmbientalUpdateDomainEvent(this.Id));

        return Result.Success();
    }

    public Result Delete(DateTime utcNow,string usuarioEliminacion)
    {
        EstadoEstudioAmbiental = (int)EstadosEnum.Eliminado;
        FechaEliminacion = utcNow;
        UsuarioEliminacion = usuarioEliminacion;
        return Result.Success();
    }
}
