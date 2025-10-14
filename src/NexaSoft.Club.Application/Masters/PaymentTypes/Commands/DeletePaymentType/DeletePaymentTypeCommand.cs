using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.Masters.PaymentTypes.Commands.DeletePaymentType;

public sealed record DeletePaymentTypeCommand(
    long Id,
    string DeletedBy
) : ICommand<bool>;
