namespace NexaSoft.Club.Domain.Abstractions;

public abstract class Entity
{
    private readonly List<IDomainEvent> _domainEvents = new();

    protected Entity(DateTime createdAt, string? createdBy, string? updatedBy, string? deletedBy)
    {
        CreatedAt = createdAt;
        CreatedBy = createdBy;
        UpdatedBy = updatedBy;
        DeletedBy = deletedBy;
    }

    protected Entity(DateTime createdAt)
    {
        CreatedAt = createdAt;
    }

    protected Entity(DateTime createdAt, String? createdBy)
    {
        CreatedAt = createdAt;
        CreatedBy = createdBy;
    }

    protected Entity() { }


    public long Id { get; init; }
    public DateTime CreatedAt { get; init; }
    //public DateTime? UltimaActualizacion { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }

    public string? CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }

    public string? DeletedBy { get; set; }


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