using NexaSoft.Agro.Domain.Abstractions;
using NexaSoft.Agro.Domain.Features.Proyectos.Capitulos.Events;
using NexaSoft.Agro.Domain.Features.Proyectos.EstudiosAmbientales;
using static NexaSoft.Agro.Domain.Shareds.Enums;

namespace NexaSoft.Agro.Domain.Features.Proyectos.Capitulos;

public class Capitulo : Entity
{
    public string? NombreCapitulo { get; private set; }
    public string? DescripcionCapitulo { get; private set; }
    public int EstadoCapitulo { get; private set; }
    public Guid EstudioAmbientalId { get; private set; }
    public EstudioAmbiental? EstudioAmbiental { get; private set; }

    //public ICollection<SubCapitulo> SubCapitulos { get; private set; } = new List<SubCapitulo>();

    private Capitulo() { }

    private Capitulo(
        Guid id,
        string? nombreCapitulo,
        string? descripcionCapitulo,
        int estadoCapitulo,
        Guid estudioAmbientalId,
        DateTime fechaCreacion
    ) : base(id, fechaCreacion)
    {
        NombreCapitulo = nombreCapitulo;
        DescripcionCapitulo = descripcionCapitulo;
        EstadoCapitulo = estadoCapitulo;
        EstudioAmbientalId = estudioAmbientalId;
        FechaCreacion = fechaCreacion;
    }

    public static Capitulo Create(
        string? nombreCapitulo,
        string? descripcionCapitulo,
        int estadoCapitulo,
        Guid estudioAmbientalId,
        DateTime fechaCreacion
    )
    {
        var entity = new Capitulo(
            Guid.NewGuid(),
            nombreCapitulo,
            descripcionCapitulo,
            estadoCapitulo,
            estudioAmbientalId,
            fechaCreacion
        );
        entity.RaiseDomainEvent(new CapituloCreateDomainEvent(entity.Id));
        return entity;
    }

    public Result Update(
        Guid Id,
        string? nombreCapitulo,
        string? descripcionCapitulo,
        Guid estudioAmbientalId,
        DateTime utcNow
    )
    {
        NombreCapitulo = nombreCapitulo;
        DescripcionCapitulo = descripcionCapitulo;
        EstudioAmbientalId = estudioAmbientalId;
        FechaModificacion = utcNow;

        RaiseDomainEvent(new CapituloUpdateDomainEvent(this.Id));

        return Result.Success();
    }

    public Result Delete(DateTime utcNow)
    {
        EstadoCapitulo = (int)EstadosEnum.Eliminado;
        FechaEliminacion = utcNow;
        return Result.Success();
    }
}
