using NexaSoft.Agro.Application.Abstractions.Messaging;

namespace NexaSoft.Agro.Application.Masters.Ubigeos.Commands.UpdateUbigeo;

public sealed record UpdateUbigeoCommand(
    Guid Id,
    string? Descripcion,
    int Nivel,
    Guid? PadreId
) : ICommand<bool>;
