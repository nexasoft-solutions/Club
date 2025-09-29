namespace NexaSoft.Club.Api.Controllers.Features.ExpensesVouchers.Request;

public sealed record DeleteExpenseVoucherRequest(
   long Id,
   string DeletedBy
);
