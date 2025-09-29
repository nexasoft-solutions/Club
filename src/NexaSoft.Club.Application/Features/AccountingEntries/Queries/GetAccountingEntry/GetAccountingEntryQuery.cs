using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Features.AccountingEntries;

namespace NexaSoft.Club.Application.Features.AccountingEntries.Queries.GetAccountingEntry;

public sealed record GetAccountingEntryQuery(
    long Id
) : IQuery<AccountingEntryResponse>;
