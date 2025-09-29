using NexaSoft.Club.Domain.Abstractions;
using static NexaSoft.Club.Domain.Shareds.Enums;

namespace NexaSoft.Club.Domain.Masters.Constantes;

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
        DateTime createdAt,
        string? createdBy,
        string? updatedBy = null,
        string? deletedBy = null
    ) : base(createdAt, createdBy, updatedBy, deletedBy)
    {
        TipoConstante = tipoConstante;
        Clave = clave;
        Valor = valor;
        EstadoConstante = estadoConstante;
        CreatedAt = createdAt;
        CreatedBy = createdBy;
        UpdatedBy = updatedBy;
        DeletedBy = deletedBy;
    }

    public static Constante Create(
        string? tipoConstante, 
        int clave, 
        string? valor, 
        int estadoConstante, 
        DateTime createdAt,
        string? createdBy
    )
    {
        var entity = new Constante(
            tipoConstante,
            clave,
            valor,
            estadoConstante,
            createdAt,
            createdBy
        );
        //entity.RaiseDomainEvent(new ConstanteCreateDomainEvent(entity.Id));
        return entity;
    }

    public Result Update(
        long Id,
        string? tipoConstante, 
        string? valor, 
        DateTime utcNow,
        string? updatedBy
    )
    {
        TipoConstante = tipoConstante;
        Valor = valor;
        UpdatedAt = utcNow;
        UpdatedBy = updatedBy;

        //RaiseDomainEvent(new ConstanteUpdateDomainEvent(this.Id));

        return Result.Success();
    }

    public Result Delete(DateTime utcNow,string deletedBy)
    {
        EstadoConstante = (int)EstadosEnum.Eliminado;
        DeletedAt = utcNow;
        DeletedBy = deletedBy;
        return Result.Success();
    }
}
