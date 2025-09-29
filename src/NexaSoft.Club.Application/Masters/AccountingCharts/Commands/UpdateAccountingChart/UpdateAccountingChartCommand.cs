using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.Masters.AccountingCharts.Commands.UpdateAccountingChart;

public sealed record UpdateAccountingChartCommand(
    long Id,
    string? AccountCode,
    string? AccountName,
    string? AccountType,
    long? ParentAccountId,
    int Level,
    bool AllowsTransactions,
    string? Description,
    string UpdatedBy
) : ICommand<bool>;
