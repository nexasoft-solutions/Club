using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.Masters.AccountingCharts.Commands.CreateAccountingChart;

public sealed record CreateAccountingChartCommand(
    string? AccountCode,
    string? AccountName,
    long AccountTypeId,
    long? ParentAccountId,
    int Level,
    bool AllowsTransactions,
    string? Description,
    string CreatedBy
) : ICommand<long>;
