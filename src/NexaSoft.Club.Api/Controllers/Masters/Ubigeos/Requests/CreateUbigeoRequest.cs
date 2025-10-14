using NexaSoft.Club.Domain.Masters.Ubigeos;

namespace NexaSoft.Club.Api.Controllers.Masters.Ubigeos.Request;

public sealed record CreateUbigeoRequest(
    string? Description,
    int Level,
    long? ParentId,
    string? CreatedBy
);
