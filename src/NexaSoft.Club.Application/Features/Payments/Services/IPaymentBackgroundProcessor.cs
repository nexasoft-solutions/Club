using NexaSoft.Club.Application.Features.Payments.Commands.CreatePayment;

namespace NexaSoft.Club.Application.Features.Payments.Services;

public interface IPaymentBackgroundProcessor
{
    Task ProcessPaymentAsync(long paymentId, CreatePaymentCommand command, CancellationToken cancellationToken);
}
