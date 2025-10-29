using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.HumanResources.TimeRequests.Commands.CreateTimeRequest;

public sealed record CreateTimeRequestCommand(
    long? EmployeeId,
    long? TimeRequestTypeId,
    DateOnly? StartDate,
    DateOnly? EndDate,
    int TotalDays,
    string Reason,
    long? StatusId,
    string CreatedBy
) : ICommand<long>;
