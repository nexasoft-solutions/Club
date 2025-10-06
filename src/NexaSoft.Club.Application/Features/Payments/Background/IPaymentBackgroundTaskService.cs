using NexaSoft.Club.Application.Features.Payments.Commands.CreatePayment;

namespace NexaSoft.Club.Application.Features.Payments.Background;

public interface IPaymentBackgroundTaskService
{
    Task QueuePaymentProcessingAsync(long paymentId, CreatePaymentCommand command, CancellationToken cancellationToken = default);
}
