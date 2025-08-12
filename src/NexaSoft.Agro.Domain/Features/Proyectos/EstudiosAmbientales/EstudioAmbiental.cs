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
    public Guid EmpresaId { get; private set; }
    public int EstadoEstudioAmbiental { get; private set; }
    public Empresa? Empresa { get; private set; }

    //public ICollection<Capitulo> Capitulos { get; private set; } = new List<Capitulo>();

    private EstudioAmbiental() { }

    private EstudioAmbiental(
        Guid id, 
        string? proyecto, 
        string? codigoEstudio,
        DateOnly fechaInicio, 
        DateOnly fechaFin, 
        string? detalles, 
        Guid empresaId, 
        int estadoEstudioAmbiental, 
        DateTime fechaCreacion
    ) : base(id, fechaCreacion)
    {
        Proyecto = proyecto;
        CodigoEstudio = codigoEstudio;
        FechaInicio = fechaInicio;
        FechaFin = fechaFin;
        Detalles = detalles;
        EmpresaId = empresaId;
        EstadoEstudioAmbiental = estadoEstudioAmbiental;
        FechaCreacion = fechaCreacion;
    }

    public static EstudioAmbiental Create(
        string? proyecto, 
        string? codigoEstudio,
        DateOnly fechaInicio, 
        DateOnly fechaFin, 
        string? detalles, 
        Guid empresaId, 
        int estadoEstudioAmbiental, 
        DateTime fechaCreacion
    )
    {
        var entity = new EstudioAmbiental(
            Guid.NewGuid(),
            proyecto,
            codigoEstudio,
            fechaInicio,
            fechaFin,
            detalles,
            empresaId,
            estadoEstudioAmbiental,
            fechaCreacion
        );
        entity.RaiseDomainEvent(new EstudioAmbientalCreateDomainEvent(entity.Id));
        return entity;
    }

    public Result Update(
        Guid Id,
        string? proyecto, 
        string? codigoEstudio,
        DateOnly fechaInicio, 
        DateOnly fechaFin, 
        string? detalles, 
        Guid empresaId, 
        DateTime utcNow
    )
    {
        Proyecto = proyecto;
        CodigoEstudio = codigoEstudio;
        FechaInicio = fechaInicio;
        FechaFin = fechaFin;
        Detalles = detalles;
        EmpresaId = empresaId;
        FechaModificacion = utcNow;

        RaiseDomainEvent(new EstudioAmbientalUpdateDomainEvent(this.Id));

        return Result.Success();
    }

    public Result Delete(DateTime utcNow)
    {
        EstadoEstudioAmbiental = (int)EstadosEnum.Eliminado;
        FechaEliminacion = utcNow;
        return Result.Success();
    }
}
