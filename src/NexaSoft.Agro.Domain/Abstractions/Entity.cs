namespace NexaSoft.Agro.Domain.Abstractions;

public abstract class Entity
{
    private readonly List<IDomainEvent> _domainEvents = new();

    protected Entity()
    {
    }

    protected Entity(DateTime fechaCreacion)
    {
         FechaCreacion = fechaCreacion;
    }

    protected Entity(Guid id, DateTime fechaCreacion)
    {
        Id = id;
        FechaCreacion = fechaCreacion;

    }

    public Guid Id { get; init; }
    public DateTime FechaCreacion { get; init; }
    //public DateTime? UltimaActualizacion { get; set; }
    public DateTime? FechaModificacion { get; set; }
    public DateTime? FechaEliminacion { get; set; }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    public void RaiseDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public IReadOnlyList<IDomainEvent> GetDomainEvents()
    {
        return _domainEvents.ToList();
    }

}