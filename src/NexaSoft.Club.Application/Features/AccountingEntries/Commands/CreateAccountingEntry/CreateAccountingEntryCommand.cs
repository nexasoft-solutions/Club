using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.Features.AccountingEntries.Commands.CreateAccountingEntry;

public sealed record CreateAccountingEntryCommand(
    string? EntryNumber,
    DateOnly EntryDate,
    string? Description,
    long SourceModuleId,
    long? SourceId,
    decimal TotalDebit,
    decimal TotalCredit,
    bool IsAdjusted,
    string? AdjustmentReason,
    string CreatedBy
) : ICommand<long>;

