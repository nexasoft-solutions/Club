namespace NexaSoft.Club.Api.Controllers.Features.AccountingEntries.Request;

public sealed record CreateAccountingEntryRequest(
    string? EntryNumber,
    DateOnly EntryDate,
    string? Description,
    string? sourceModule,
    long? sourceId,
    decimal TotalDebit,
    decimal TotalCredit,
    bool isAdjusted,
    string? adjustmentReason,
    string CreatedBy
);

