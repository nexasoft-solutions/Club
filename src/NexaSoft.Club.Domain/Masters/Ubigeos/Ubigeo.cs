using NexaSoft.Club.Domain.Abstractions;
using static NexaSoft.Club.Domain.Shareds.Enums;

namespace NexaSoft.Club.Domain.Masters.Ubigeos;

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
        DateTime createdAt,
        string? createdBy,
        string? updatedBy = null,
        string? deletedBy = null
    ) : base(createdAt, createdBy, updatedBy, deletedBy)
    {
        Descripcion = descripcion;
        Nivel = nivel;
        PadreId = padreId;
        EstadoUbigeo = estadoUbigeo;
        CreatedAt = createdAt;
        CreatedBy = createdBy;
        UpdatedBy = updatedBy;
        DeletedBy = deletedBy;
    }

    public static Ubigeo Create(
        string? descripcion, 
        int nivel, 
        long? padreId,
        int estadoUbigeo, 
        DateTime createdAt,
        string? createdBy
    )
    {
        var entity = new Ubigeo(
            descripcion,
            nivel,
            padreId,
            estadoUbigeo,
            createdAt,
            createdBy
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
        string? updatedBy
    )
    {
        Descripcion = descripcion;
        Nivel = nivel;
        PadreId = padreId;        
        UpdatedAt = utcNow;
        UpdatedBy = updatedBy;

        //RaiseDomainEvent(new UbigeoUpdateDomainEvent(this.Id));

        return Result.Success();
    }

    public Result Delete(DateTime utcNow, string deletedBy)
    {
        EstadoUbigeo = (int)EstadosEnum.Eliminado;
        DeletedAt = utcNow;
        DeletedBy = deletedBy;
        return Result.Success();
    }
}
