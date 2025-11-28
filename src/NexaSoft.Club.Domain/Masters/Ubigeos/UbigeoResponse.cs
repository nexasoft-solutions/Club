
namespace NexaSoft.Club.Domain.Masters.Ubigeos;

public sealed record UbigeoResponse(
    long Id,
    string? Description,
    long Level,
    string LevelDescription,
    long? ParentId,
    string? ParentDescription,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    string? CreatedBy,
    string? UpdatedBy,
    List<UbigeoResponse>? Children = null 
);
