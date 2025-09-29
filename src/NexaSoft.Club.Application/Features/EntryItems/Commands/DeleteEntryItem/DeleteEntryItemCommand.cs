using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.Features.EntryItems.Commands.DeleteEntryItem;

public sealed record DeleteEntryItemCommand(
    long Id,
    string DeletedBy
) : ICommand<bool>;
