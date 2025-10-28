using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.HumanResources.PayPeriodTypes.Commands.DeletePayPeriodType;

public sealed record DeletePayPeriodTypeCommand(
    long Id,
    string DeletedBy
) : ICommand<bool>;
