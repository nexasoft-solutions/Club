using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.Features.EntryItems;

namespace NexaSoft.Club.Application.Features.EntryItems.Queries.GetEntryItems;

public sealed record GetEntryItemsQuery(
    BaseSpecParams SpecParams
) : IQuery<Pagination<EntryItemResponse>>;
