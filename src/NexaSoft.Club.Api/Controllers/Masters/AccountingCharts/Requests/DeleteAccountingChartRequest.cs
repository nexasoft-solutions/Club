namespace NexaSoft.Club.Api.Controllers.Masters.AccountingCharts.Request;

public sealed record DeleteAccountingChartRequest(
   long Id,
   string DeletedBy
);
