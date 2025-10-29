using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.HumanResources.EmploymentContracts.Commands.DeleteEmploymentContract;

public sealed record DeleteEmploymentContractCommand(
    long Id,
    string DeletedBy
) : ICommand<bool>;
