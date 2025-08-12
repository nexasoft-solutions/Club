using NexaSoft.Agro.Domain.Abstractions;
using NexaSoft.Agro.Domain.Masters.Constantes.Events;
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
        Guid id, 
        string? tipoConstante, 
        int clave, 
        string? valor, 
        int estadoConstante, 
        DateTime fechaCreacion
    ) : base(id, fechaCreacion)
    {
        TipoConstante = tipoConstante;
        Clave = clave;
        Valor = valor;
        EstadoConstante = estadoConstante;
        FechaCreacion = fechaCreacion;
    }

    public static Constante Create(
        string? tipoConstante, 
        int clave, 
        string? valor, 
        int estadoConstante, 
        DateTime fechaCreacion
    )
    {
        var entity = new Constante(
            Guid.NewGuid(),
            tipoConstante,
            clave,
            valor,
            estadoConstante,
            fechaCreacion
        );
        entity.RaiseDomainEvent(new ConstanteCreateDomainEvent(entity.Id));
        return entity;
    }

    public Result Update(
        Guid Id,
        string? tipoConstante, 
        string? valor, 
        DateTime utcNow
    )
    {
        TipoConstante = tipoConstante;
        Valor = valor;
        FechaModificacion = utcNow;

        RaiseDomainEvent(new ConstanteUpdateDomainEvent(this.Id));

        return Result.Success();
    }

    public Result Delete(DateTime utcNow)
    {
        EstadoConstante = (int)EstadosEnum.Eliminado;
        FechaEliminacion = utcNow;
        return Result.Success();
    }
}
