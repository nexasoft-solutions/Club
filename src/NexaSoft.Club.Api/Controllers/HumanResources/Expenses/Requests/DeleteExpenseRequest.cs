namespace NexaSoft.Club.Api.Controllers.HumanResources.Expenses.Request;

public sealed record DeleteExpenseRequest(
   long Id,
   string DeletedBy
);
