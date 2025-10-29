using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.HumanResources.PayrollPeriods.Commands.CreatePayrollPeriod;

public sealed record CreatePayrollPeriodCommand(
    string? PeriodName,
    DateOnly? StartDate,
    DateOnly? EndDate,
    decimal TotalAmount,
    int? TotalEmployees,
    long? StatusId,
    string CreatedBy
) : ICommand<long>;
