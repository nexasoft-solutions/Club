using System.Runtime.InteropServices;
using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.Masters.Ubigeos.Commands.CreateUbigeo;

public sealed record CreateUbigeoCommand(
    string? Descripcion,
    int Nivel,
    long? PadreId,
    string? UsuarioCreacion
) : ICommand<long>;
