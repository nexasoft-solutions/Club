using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.Features.Payments.Events;

public sealed record PaymentAccountingEntryGeneratedDomainEvent(
    Guid Id,
    long PaymentId,
    long AccountingEntryId,
    string CreatedBy,
    DateTime CreatedOn
) : IDomainEvent;