using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.Features.EntryItems.Commands.UpdateEntryItem;

public sealed record UpdateEntryItemCommand(
    long Id,
    long EntryId,
    long AccountId,
    decimal DebitAmount,
    decimal CreditAmount,
    string? Description,
    string UpdatedBy
) : ICommand<bool>;
