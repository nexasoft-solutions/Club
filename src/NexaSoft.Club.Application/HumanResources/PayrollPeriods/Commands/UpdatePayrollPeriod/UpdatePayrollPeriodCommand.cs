using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.HumanResources.PayrollPeriods.Commands.UpdatePayrollPeriod;

public sealed record UpdatePayrollPeriodCommand(
    long Id,
    string? PeriodName,
    DateOnly? StartDate,
    DateOnly? EndDate,
    decimal TotalAmount,
    int? TotalEmployees,
    long? StatusId,
    string UpdatedBy
) : ICommand<bool>;
