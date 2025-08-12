using NexaSoft.Agro.Application.Abstractions.Messaging;

namespace NexaSoft.Agro.Application.Masters.Consultoras.Colaboradores.Commands.DeleteColaborador;

public sealed record DeleteColaboradorCommand(
    Guid Id
) : ICommand<bool>;
