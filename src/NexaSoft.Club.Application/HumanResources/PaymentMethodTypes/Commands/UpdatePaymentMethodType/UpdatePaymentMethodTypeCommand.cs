using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.HumanResources.PaymentMethodTypes.Commands.UpdatePaymentMethodType;

public sealed record UpdatePaymentMethodTypeCommand(
    long Id,
    string? Code,
    string? Name,
    string? Description,
    string UpdatedBy
) : ICommand<bool>;
