namespace NexaSoft.Club.Domain.Features.Payments;

public record class PaymentItemsResponse
(
    long Id,
    long PaymentId,
    long MemberFeeId,
    decimal Amount
);
