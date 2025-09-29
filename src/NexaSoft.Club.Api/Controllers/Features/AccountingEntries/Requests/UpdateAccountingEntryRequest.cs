namespace NexaSoft.Club.Api.Controllers.Features.AccountingEntries.Request;

public sealed record UpdateAccountingEntryRequest(
   long Id,
    string? EntryNumber,
    DateOnly EntryDate,
    string? Description,
    string? sourceModule,
    long? sourceId,
    decimal TotalDebit,
    decimal TotalCredit,
    bool IsAdjusted,
    string? AdjustmentReason,
    string UpdatedBy
);
