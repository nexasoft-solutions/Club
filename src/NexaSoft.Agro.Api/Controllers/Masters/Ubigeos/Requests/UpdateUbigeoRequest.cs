using NexaSoft.Agro.Domain.Masters.Ubigeos;

namespace NexaSoft.Agro.Api.Controllers.Masters.Ubigeos.Request;

public sealed record UpdateUbigeoRequest(
   Guid Id,
    string? Descripcion,
    int Nivel,
    Guid? PadreId
);
