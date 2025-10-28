namespace NexaSoft.Club.Api.Controllers.HumanResources.BankAccountTypes.Request;

public sealed record CreateBankAccountTypeRequest(
    string? Code,
    string? Name,
    string? Description,
    string CreatedBy
);
