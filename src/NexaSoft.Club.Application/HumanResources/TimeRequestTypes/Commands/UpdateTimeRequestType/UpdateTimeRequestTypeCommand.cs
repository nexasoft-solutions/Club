using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.HumanResources.TimeRequestTypes.Commands.UpdateTimeRequestType;

public sealed record UpdateTimeRequestTypeCommand(
    long Id,
    string? Code,
    string? Name,
    bool DeductsSalary,
    bool RequiresApproval,
    string? Description,
    string UpdatedBy
) : ICommand<bool>;
