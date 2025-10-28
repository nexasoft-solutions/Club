namespace NexaSoft.Club.Api.Controllers.HumanResources.BankAccountTypes.Request;

public sealed record DeleteBankAccountTypeRequest(
   long Id,
   string DeletedBy
);
