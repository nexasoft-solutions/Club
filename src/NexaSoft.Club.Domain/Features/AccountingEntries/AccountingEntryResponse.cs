namespace NexaSoft.Club.Domain.Features.AccountingEntries;

public sealed record AccountingEntryResponse(
    long Id,
    string? EntryNumber,
    DateOnly EntryDate,
    string? Description,
    string? SourceModule,
    long? SourceId,
    decimal TotalDebit,
    decimal TotalCredit,
    bool IsAdjusted,
    string? AdjustmentReason,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    string? CreatedBy,
    string? UpdatedBy
);
