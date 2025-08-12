using System.Runtime.InteropServices;
using NexaSoft.Agro.Application.Abstractions.Messaging;

namespace NexaSoft.Agro.Application.Masters.Ubigeos.Commands.CreateUbigeo;

public sealed record CreateUbigeoCommand(
    string? Descripcion,
    int Nivel,
    Guid? PadreId
) : ICommand<Result<Guid>>;
