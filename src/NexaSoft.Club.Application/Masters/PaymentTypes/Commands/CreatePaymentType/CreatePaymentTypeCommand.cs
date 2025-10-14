using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.Masters.PaymentTypes.Commands.CreatePaymentType;

public sealed record CreatePaymentTypeCommand(
    string? Name,
    string? Description,
    string CreatedBy
) : ICommand<long>;
