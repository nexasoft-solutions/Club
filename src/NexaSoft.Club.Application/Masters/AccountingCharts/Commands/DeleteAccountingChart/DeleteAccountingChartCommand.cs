using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.Masters.AccountingCharts.Commands.DeleteAccountingChart;

public sealed record DeleteAccountingChartCommand(
    long Id,
    string DeletedBy
) : ICommand<bool>;
