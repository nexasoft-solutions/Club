using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Features.AccountingEntries;

namespace NexaSoft.Club.Application.Features.AccountingEntries.Queries.GetAccountingEntriesBySource;

public sealed record GetAccountingEntriesBySourceQuery
(
    long SourceModuleId,
    long SourceId
) : IQuery<AccountingEntryResponse>;