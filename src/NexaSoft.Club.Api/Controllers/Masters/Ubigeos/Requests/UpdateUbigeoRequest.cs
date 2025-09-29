using NexaSoft.Club.Domain.Masters.Ubigeos;

namespace NexaSoft.Club.Api.Controllers.Masters.Ubigeos.Request;

public sealed record UpdateUbigeoRequest(
   long Id,
    string? Descripcion,
    int Nivel,
    long? PadreId,
    string? UsuarioModificacion
);
