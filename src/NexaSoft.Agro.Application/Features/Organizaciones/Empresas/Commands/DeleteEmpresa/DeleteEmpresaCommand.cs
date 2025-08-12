using NexaSoft.Agro.Application.Abstractions.Messaging;

namespace NexaSoft.Agro.Application.Features.Organizaciones.Empresas.Commands.DeleteEmpresa;

public sealed record DeleteEmpresaCommand(
    Guid Id
) : ICommand<bool>;
