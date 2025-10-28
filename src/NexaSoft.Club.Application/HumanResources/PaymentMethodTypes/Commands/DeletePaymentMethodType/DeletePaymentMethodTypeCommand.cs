using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.HumanResources.PaymentMethodTypes.Commands.DeletePaymentMethodType;

public sealed record DeletePaymentMethodTypeCommand(
    long Id,
    string DeletedBy
) : ICommand<bool>;
