namespace NexaSoft.Club.Api.Controllers.HumanResources.CompanyBankAccounts.Request;

public sealed record DeleteCompanyBankAccountRequest(
   long Id,
   string DeletedBy
);
