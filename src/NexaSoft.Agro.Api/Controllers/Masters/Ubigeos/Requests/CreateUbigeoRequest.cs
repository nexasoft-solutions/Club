using NexaSoft.Agro.Domain.Masters.Ubigeos;

namespace NexaSoft.Agro.Api.Controllers.Masters.Ubigeos.Request;

public sealed record CreateUbigeoRequest(
    string? Descripcion,
    int Nivel,
    long? PadreId,
    string? UsuarioCreacion
);
