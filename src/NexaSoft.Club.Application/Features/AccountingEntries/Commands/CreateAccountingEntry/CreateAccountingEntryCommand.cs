using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.Features.AccountingEntries.Commands.CreateAccountingEntry;

public sealed record CreateAccountingEntryCommand(
    string? EntryNumber,
    DateOnly EntryDate,
    string? Description,
    string? sourceModule,
    long? sourceId,
    decimal TotalDebit,
    decimal TotalCredit,
    bool isAdjusted,
    string? adjustmentReason,
    string CreatedBy
) : ICommand<long>;

