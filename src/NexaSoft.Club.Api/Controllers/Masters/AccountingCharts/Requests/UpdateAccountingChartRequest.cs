namespace NexaSoft.Club.Api.Controllers.Masters.AccountingCharts.Request;

public sealed record UpdateAccountingChartRequest(
   long Id,
    string? AccountCode,
    string? AccountName,
    long AccountTypeId,
    long? ParentAccountId,
    int Level,
    bool AllowsTransactions,
    string? Description,
    string UpdatedBy
);
