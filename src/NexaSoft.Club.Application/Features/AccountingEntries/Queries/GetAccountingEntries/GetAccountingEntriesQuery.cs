using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.Features.AccountingEntries;

namespace NexaSoft.Club.Application.Features.AccountingEntries.Queries.GetAccountingEntries;

public sealed record GetAccountingEntriesQuery(
    BaseSpecParams SpecParams
) : IQuery<Pagination<AccountingEntryResponse>>;
