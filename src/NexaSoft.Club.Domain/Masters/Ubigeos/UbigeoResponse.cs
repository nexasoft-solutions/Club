
namespace NexaSoft.Club.Domain.Masters.Ubigeos;

public sealed record UbigeoResponse(
    long Id,
    string? Description,
    string Level,
    long? ParentId,
    string? ParentDescription,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    string? CreatedBy,
    string? UpdatedBy
);
