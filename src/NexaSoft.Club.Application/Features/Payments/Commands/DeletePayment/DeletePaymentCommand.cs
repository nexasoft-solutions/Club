using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.Features.Payments.Commands.DeletePayment;

public sealed record DeletePaymentCommand(
    long Id,
    string DeletedBy
) : ICommand<bool>;
