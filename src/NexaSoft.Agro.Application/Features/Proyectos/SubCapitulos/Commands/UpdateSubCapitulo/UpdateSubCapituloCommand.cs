using NexaSoft.Agro.Application.Abstractions.Messaging;

namespace NexaSoft.Agro.Application.Features.Proyectos.SubCapitulos.Commands.UpdateSubCapitulo;

public sealed record UpdateSubCapituloCommand(
    long Id,
    string? NombreSubCapitulo,
    string? DescripcionSubCapitulo,
    long CapituloId,
    string? UsuarioModificacion
) : ICommand<bool>;
