using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.HumanResources.ContractTypes.Commands.DeleteContractType;

public sealed record DeleteContractTypeCommand(
    long Id,
    string DeletedBy
) : ICommand<bool>;
