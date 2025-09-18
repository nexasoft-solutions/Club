using NexaSoft.Agro.Application.Abstractions.Messaging;

namespace NexaSoft.Agro.Application.Masters.Ubigeos.Commands.UpdateUbigeo;

public sealed record UpdateUbigeoCommand(
    long Id,
    string? Descripcion,
    int Nivel,
    long? PadreId,
    string UsuarioModificacion
) : ICommand<bool>;
