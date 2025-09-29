using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.Features.EntryItems.Commands.CreateEntryItem;

public sealed record CreateEntryItemCommand(
    long EntryId,
    long AccountId,
    decimal DebitAmount,
    decimal CreditAmount,
    string? Description,
    string CreatedBy
) : ICommand<long>;
