using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.HumanResources.PayPeriodTypes.Commands.CreatePayPeriodType;

public sealed record CreatePayPeriodTypeCommand(
    string? Code,
    string? Name,
    int? Days,
    string? Description,
    string CreatedBy
) : ICommand<long>;
