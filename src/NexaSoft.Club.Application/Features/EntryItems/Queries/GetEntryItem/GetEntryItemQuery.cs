using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Features.EntryItems;

namespace NexaSoft.Club.Application.Features.EntryItems.Queries.GetEntryItem;

public sealed record GetEntryItemQuery(
    long Id
) : IQuery<EntryItemResponse>;
