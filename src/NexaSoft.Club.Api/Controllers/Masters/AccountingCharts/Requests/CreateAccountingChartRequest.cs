namespace NexaSoft.Club.Api.Controllers.Masters.AccountingCharts.Request;

public sealed record CreateAccountingChartRequest(
    string? AccountCode,
    string? AccountName,
    string? AccountType,
    long? ParentAccountId,
    int Level,
    bool AllowsTransactions,
    string? Description,
    string CreatedBy
);
