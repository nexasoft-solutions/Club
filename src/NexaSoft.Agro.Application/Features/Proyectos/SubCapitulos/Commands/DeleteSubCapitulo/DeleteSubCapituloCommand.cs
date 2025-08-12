using NexaSoft.Agro.Application.Abstractions.Messaging;

namespace NexaSoft.Agro.Application.Features.Proyectos.SubCapitulos.Commands.DeleteSubCapitulo;

public sealed record DeleteSubCapituloCommand(
    Guid Id
) : ICommand<bool>;
