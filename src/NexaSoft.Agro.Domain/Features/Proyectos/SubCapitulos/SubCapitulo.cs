using NexaSoft.Agro.Domain.Abstractions;
using NexaSoft.Agro.Domain.Features.Proyectos.SubCapitulos.Events;
using static NexaSoft.Agro.Domain.Shareds.Enums;
using NexaSoft.Agro.Domain.Features.Proyectos.Capitulos;

namespace NexaSoft.Agro.Domain.Features.Proyectos.SubCapitulos;

public class SubCapitulo : Entity
{
    public string? NombreSubCapitulo { get; private set; }
    public string? DescripcionSubCapitulo { get; private set; }
    public long CapituloId { get; private set; }
    public int EstadoSubCapitulo { get; private set; }
    public Capitulo? Capitulo { get; private set; }

    private SubCapitulo() { }

    private SubCapitulo(
        string? nombreSubCapitulo,
        string? descripcionSubCapitulo,
        long capituloId,
        int estadoSubCapitulo,
        DateTime fechaCreacion,
        string? usuarioCreacion,
        string? usuarioModificacion = null,
        string? usuarioEliminacion = null
    ) : base(fechaCreacion, usuarioCreacion, usuarioModificacion, usuarioEliminacion)
    {
        NombreSubCapitulo = nombreSubCapitulo;
        DescripcionSubCapitulo = descripcionSubCapitulo;
        CapituloId = capituloId;
        EstadoSubCapitulo = estadoSubCapitulo;
        FechaCreacion = fechaCreacion;
        UsuarioCreacion = usuarioCreacion;
        UsuarioModificacion = usuarioModificacion;
        UsuarioEliminacion = usuarioEliminacion;
    }

    public static SubCapitulo Create(
        string? nombreSubCapitulo, 
        string? descripcionSubCapitulo, 
        long capituloId, 
        int estadoSubCapitulo, 
        DateTime fechaCreacion,
        string? usuarioCreacion
    )
    {
        var entity = new SubCapitulo(
            nombreSubCapitulo,
            descripcionSubCapitulo,
            capituloId,
            estadoSubCapitulo,
            fechaCreacion,
            usuarioCreacion
        );
        //entity.RaiseDomainEvent(new SubCapituloCreateDomainEvent(entity.Id));
        return entity;
    }

    public Result Update(
        long Id,
        string? nombreSubCapitulo, 
        string? descripcionSubCapitulo, 
        long capituloId, 
        DateTime utcNow,
        string? usuarioModificacion
    )
    {
        NombreSubCapitulo = nombreSubCapitulo;
        DescripcionSubCapitulo = descripcionSubCapitulo;
        CapituloId = capituloId;
        FechaModificacion = utcNow;
        UsuarioModificacion = usuarioModificacion;

        //RaiseDomainEvent(new SubCapituloUpdateDomainEvent(this.Id));

        return Result.Success();
    }

    public Result Delete(DateTime utcNow,string usuarioEliminacion)
    {
        EstadoSubCapitulo = (int)EstadosEnum.Eliminado;
        FechaEliminacion = utcNow;
        UsuarioEliminacion = usuarioEliminacion;
        return Result.Success();
    }
}
