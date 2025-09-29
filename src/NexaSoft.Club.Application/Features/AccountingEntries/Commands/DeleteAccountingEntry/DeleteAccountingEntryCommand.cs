using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.Features.AccountingEntries.Commands.DeleteAccountingEntry;

public sealed record DeleteAccountingEntryCommand(
    long Id,
    string DeletedBy
) : ICommand<bool>;
