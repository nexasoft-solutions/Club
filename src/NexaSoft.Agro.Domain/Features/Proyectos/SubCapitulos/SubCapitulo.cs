using NexaSoft.Agro.Domain.Abstractions;
using NexaSoft.Agro.Domain.Features.Proyectos.SubCapitulos.Events;
using static NexaSoft.Agro.Domain.Shareds.Enums;
using NexaSoft.Agro.Domain.Features.Proyectos.Capitulos;

namespace NexaSoft.Agro.Domain.Features.Proyectos.SubCapitulos;

public class SubCapitulo : Entity
{
    public string? NombreSubCapitulo { get; private set; }
    public string? DescripcionSubCapitulo { get; private set; }
    public Guid CapituloId { get; private set; }
    public int EstadoSubCapitulo { get; private set; }
    public Capitulo? Capitulo { get; private set; }

    //public ICollection<Estructura> Estructuras { get; private set; } = new List<Estructura>();
    //public ICollection<Archivo> Archivos { get; private set; } = new List<Archivo>();

    private SubCapitulo() { }

    private SubCapitulo(
        Guid id, 
        string? nombreSubCapitulo, 
        string? descripcionSubCapitulo, 
        Guid capituloId, 
        int estadoSubCapitulo, 
        DateTime fechaCreacion
    ) : base(id, fechaCreacion)
    {
        NombreSubCapitulo = nombreSubCapitulo;
        DescripcionSubCapitulo = descripcionSubCapitulo;
        CapituloId = capituloId;
        EstadoSubCapitulo = estadoSubCapitulo;
        FechaCreacion = fechaCreacion;
    }

    public static SubCapitulo Create(
        string? nombreSubCapitulo, 
        string? descripcionSubCapitulo, 
        Guid capituloId, 
        int estadoSubCapitulo, 
        DateTime fechaCreacion
    )
    {
        var entity = new SubCapitulo(
            Guid.NewGuid(),
            nombreSubCapitulo,
            descripcionSubCapitulo,
            capituloId,
            estadoSubCapitulo,
            fechaCreacion
        );
        entity.RaiseDomainEvent(new SubCapituloCreateDomainEvent(entity.Id));
        return entity;
    }

    public Result Update(
        Guid Id,
        string? nombreSubCapitulo, 
        string? descripcionSubCapitulo, 
        Guid capituloId, 
        DateTime utcNow
    )
    {
        NombreSubCapitulo = nombreSubCapitulo;
        DescripcionSubCapitulo = descripcionSubCapitulo;
        CapituloId = capituloId;
        FechaModificacion = utcNow;

        RaiseDomainEvent(new SubCapituloUpdateDomainEvent(this.Id));

        return Result.Success();
    }

    public Result Delete(DateTime utcNow)
    {
        EstadoSubCapitulo = (int)EstadosEnum.Eliminado;
        FechaEliminacion = utcNow;
        return Result.Success();
    }
}
