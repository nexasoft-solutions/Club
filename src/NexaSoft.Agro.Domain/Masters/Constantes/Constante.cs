using NexaSoft.Agro.Domain.Abstractions;
using static NexaSoft.Agro.Domain.Shareds.Enums;

namespace NexaSoft.Agro.Domain.Masters.Constantes;

public class Constante : Entity
{
    public string? TipoConstante { get; private set; }
    public int Clave { get; private set; }
    public string? Valor { get; private set; }
    public int EstadoConstante { get; private set; }

    private Constante() { }

    private Constante(
        string? tipoConstante,
        int clave,
        string? valor,
        int estadoConstante,
        DateTime fechaCreacion,
        string? usuarioCreacion,
        string? usuarioModificacion = null,
        string? usuarioEliminacion = null
    ) : base(fechaCreacion, usuarioCreacion, usuarioModificacion, usuarioEliminacion)
    {
        TipoConstante = tipoConstante;
        Clave = clave;
        Valor = valor;
        EstadoConstante = estadoConstante;
        FechaCreacion = fechaCreacion;
        UsuarioCreacion = usuarioCreacion;
        UsuarioModificacion = usuarioModificacion;
        UsuarioEliminacion = usuarioEliminacion;
    }

    public static Constante Create(
        string? tipoConstante, 
        int clave, 
        string? valor, 
        int estadoConstante, 
        DateTime fechaCreacion,
        string? usuarioCreacion
    )
    {
        var entity = new Constante(
            tipoConstante,
            clave,
            valor,
            estadoConstante,
            fechaCreacion,
            usuarioCreacion
        );
        //entity.RaiseDomainEvent(new ConstanteCreateDomainEvent(entity.Id));
        return entity;
    }

    public Result Update(
        long Id,
        string? tipoConstante, 
        string? valor, 
        DateTime utcNow,
        string? usuarioModificacion
    )
    {
        TipoConstante = tipoConstante;
        Valor = valor;
        FechaModificacion = utcNow;
        UsuarioModificacion = usuarioModificacion;

        //RaiseDomainEvent(new ConstanteUpdateDomainEvent(this.Id));

        return Result.Success();
    }

    public Result Delete(DateTime utcNow,string usuarioEliminacion)
    {
        EstadoConstante = (int)EstadosEnum.Eliminado;
        FechaEliminacion = utcNow;
        UsuarioEliminacion = usuarioEliminacion;
        return Result.Success();
    }
}
