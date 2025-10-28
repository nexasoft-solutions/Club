using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.HumanResources.PayPeriodTypes.Commands.UpdatePayPeriodType;

public sealed record UpdatePayPeriodTypeCommand(
    long Id,
    string? Code,
    string? Name,
    int? Days,
    string? Description,
    string UpdatedBy
) : ICommand<bool>;
