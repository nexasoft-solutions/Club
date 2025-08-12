using NexaSoft.Agro.Application.Abstractions.Messaging;

namespace NexaSoft.Agro.Application.Features.Proyectos.SubCapitulos.Commands.UpdateSubCapitulo;

public sealed record UpdateSubCapituloCommand(
    Guid Id,
    string? NombreSubCapitulo,
    string? DescripcionSubCapitulo,
    Guid CapituloId
) : ICommand<bool>;
