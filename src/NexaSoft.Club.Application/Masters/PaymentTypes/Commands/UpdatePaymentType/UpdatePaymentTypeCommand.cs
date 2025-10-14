using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.Masters.PaymentTypes.Commands.UpdatePaymentType;

public sealed record UpdatePaymentTypeCommand(
    long Id,
    string? Name,
    string? Description,
    string UpdatedBy
) : ICommand<bool>;
