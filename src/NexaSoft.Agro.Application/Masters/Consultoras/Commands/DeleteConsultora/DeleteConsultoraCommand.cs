using NexaSoft.Agro.Application.Abstractions.Messaging;

namespace NexaSoft.Agro.Application.Masters.Consultoras.Commands.DeleteConsultora;

public sealed record DeleteConsultoraCommand(
    Guid Id
) : ICommand<bool>;
