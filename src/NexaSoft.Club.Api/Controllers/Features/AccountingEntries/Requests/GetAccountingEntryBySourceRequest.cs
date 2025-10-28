namespace NexaSoft.Club.Api.Controllers.Features.AccountingEntries.Requests;

public sealed record GetAccountingEntryBySourceRequest
(
    long SourceModuleId,
    long SourceId
);
