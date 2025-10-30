using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.HumanResources.LegalParameters.Commands.DeleteLegalParameter;

public sealed record DeleteLegalParameterCommand(
    long Id,
    string DeletedBy
) : ICommand<bool>;
