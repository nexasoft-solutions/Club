using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.HumanResources.TimeRequests.Commands.UpdateTimeRequest;

public sealed record UpdateTimeRequestCommand(
    long Id,
    long? EmployeeId,
    long? TimeRequestTypeId,
    DateOnly? StartDate,
    DateOnly? EndDate,
    int TotalDays,
    string Reason,
    long? StatusId,
    string UpdatedBy
) : ICommand<bool>;
