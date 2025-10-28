namespace NexaSoft.Club.Api.Controllers.HumanResources.BankAccountTypes.Request;

public sealed record UpdateBankAccountTypeRequest(
   long Id,
    string? Code,
    string? Name,
    string? Description,
    string UpdatedBy
);
