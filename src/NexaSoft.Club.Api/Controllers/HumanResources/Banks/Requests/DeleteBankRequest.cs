namespace NexaSoft.Club.Api.Controllers.HumanResources.Banks.Request;

public sealed record DeleteBankRequest(
   long Id,
   string DeletedBy
);
