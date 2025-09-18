using NexaSoft.Agro.Domain.Abstractions;
using static NexaSoft.Agro.Domain.Shareds.Enums;

namespace NexaSoft.Agro.Domain.Masters.Ubigeos;

public class Ubigeo : Entity
{
    public string? Descripcion { get; private set; }
    public int Nivel { get; private set; }
    public long? PadreId { get; private set; }
    public Ubigeo? Padre { get; private set; }
    public int EstadoUbigeo { get; private set; }

    private Ubigeo() { }

    private Ubigeo(
        string? descripcion,
        int nivel,
        long? padreId,
        int estadoUbigeo,
        DateTime fechaCreacion,
        string? usuarioCreacion,
        string? usuarioModificacion = null,
        string? usuarioEliminacion = null
    ) : base(fechaCreacion, usuarioCreacion, usuarioModificacion, usuarioEliminacion)
    {
        Descripcion = descripcion;
        Nivel = nivel;
        PadreId = padreId;
        EstadoUbigeo = estadoUbigeo;
        FechaCreacion = fechaCreacion;
        UsuarioCreacion = usuarioCreacion;
        UsuarioModificacion = usuarioModificacion;
        UsuarioEliminacion = usuarioEliminacion;
    }

    public static Ubigeo Create(
        string? descripcion, 
        int nivel, 
        long? padreId,
        int estadoUbigeo, 
        DateTime fechaCreacion,
        string? usuarioCreacion
    )
    {
        var entity = new Ubigeo(
            descripcion,
            nivel,
            padreId,
            estadoUbigeo,
            fechaCreacion,
            usuarioCreacion
        );
        //entity.RaiseDomainEvent(new UbigeoCreateDomainEvent(entity.Id));
        return entity;
    }

    public Result Update(
        long Id,
        string? descripcion,
        int nivel,
        long? padreId,
        DateTime utcNow,
        string? usuarioModificacion
    )
    {
        Descripcion = descripcion;
        Nivel = nivel;
        PadreId = padreId;        
        FechaModificacion = utcNow;
        UsuarioModificacion = usuarioModificacion;

        //RaiseDomainEvent(new UbigeoUpdateDomainEvent(this.Id));

        return Result.Success();
    }

    public Result Delete(DateTime utcNow, string usuarioEliminacion)
    {
        EstadoUbigeo = (int)EstadosEnum.Eliminado;
        FechaEliminacion = utcNow;
        UsuarioEliminacion = usuarioEliminacion;
        return Result.Success();
    }
}
