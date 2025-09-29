using NexaSoft.Club.Domain.Masters.Ubigeos;

namespace NexaSoft.Club.Api.Controllers.Masters.Ubigeos.Request;

public sealed record CreateUbigeoRequest(
    string? Descripcion,
    int Nivel,
    long? PadreId,
    string? UsuarioCreacion
);
