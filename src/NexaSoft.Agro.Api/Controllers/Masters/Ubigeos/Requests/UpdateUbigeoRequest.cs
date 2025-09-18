using NexaSoft.Agro.Domain.Masters.Ubigeos;

namespace NexaSoft.Agro.Api.Controllers.Masters.Ubigeos.Request;

public sealed record UpdateUbigeoRequest(
   long Id,
    string? Descripcion,
    int Nivel,
    long? PadreId,
    string? UsuarioModificacion
);
