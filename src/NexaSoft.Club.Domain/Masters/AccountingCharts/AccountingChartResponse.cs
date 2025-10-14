namespace NexaSoft.Club.Domain.Masters.AccountingCharts;

public sealed record AccountingChartResponse(
    long Id,
    string? AccountCode,
    string? AccountName,
    long? AccountTypeId,
    string? AccountTypeName,
    long? ParentAccountId,
    string? ParentAccountName,
    int Level,
    bool AllowsTransactions,
    string? Description,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    string? CreatedBy,
    string? UpdatedBy
);
