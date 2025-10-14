namespace NexaSoft.Club.Domain.Masters.PaymentTypes;

public sealed record PaymentTypeResponse(
    long Id,
    string? Name,
    string? Description,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    string? CreatedBy,
    string? UpdatedBy
);
