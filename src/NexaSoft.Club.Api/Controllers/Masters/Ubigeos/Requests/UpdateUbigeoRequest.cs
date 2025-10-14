using NexaSoft.Club.Domain.Masters.Ubigeos;

namespace NexaSoft.Club.Api.Controllers.Masters.Ubigeos.Request;

public sealed record UpdateUbigeoRequest(
   long Id,
    string? Description,
    int Level,
    long? ParentId,
    string? UserModification
);
