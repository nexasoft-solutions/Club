namespace NexaSoft.Club.Domain.HumanResources.PaymentMethodTypes;

public sealed record PaymentMethodTypeResponse(
    long Id,
    string? Code,
    string? Name,
    string? Description,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    string? CreatedBy,
    string? UpdatedBy
);
