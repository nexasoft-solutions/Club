using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.HumanResources.TimeRequestTypes.Commands.CreateTimeRequestType;

public sealed record CreateTimeRequestTypeCommand(
    string? Code,
    string? Name,
    bool DeductsSalary,
    bool RequiresApproval,
    string? Description,
    string CreatedBy
) : ICommand<long>;
