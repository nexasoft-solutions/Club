using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.Features.AccountingEntries.Commands.UpdateAccountingEntry;

public sealed record UpdateAccountingEntryCommand(
    long Id,
    string? EntryNumber,
    DateOnly EntryDate,
    string? Description,
    decimal TotalDebit,
    decimal TotalCredit,
    bool IsAdjusted,
    string? AdjustmentReason,
    string UpdatedBy
) : ICommand<bool>;
