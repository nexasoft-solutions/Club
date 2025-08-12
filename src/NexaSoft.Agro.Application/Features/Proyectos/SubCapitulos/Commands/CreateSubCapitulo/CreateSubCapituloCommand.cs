using NexaSoft.Agro.Application.Abstractions.Messaging;

namespace NexaSoft.Agro.Application.Features.Proyectos.SubCapitulos.Commands.CreateSubCapitulo;

public sealed record CreateSubCapituloCommand(
    string? NombreSubCapitulo,
    string? DescripcionSubCapitulo,
    Guid CapituloId
) : ICommand<Guid>;
