namespace NexaSoft.Club.Api.Controllers.Features.AccountingEntries.Request;

public sealed record DeleteAccountingEntryRequest(
   long Id,
   string DeletedBy
);
