namespace NexaSoft.Club.Api.Controllers.HumanResources.Banks.Request;

public sealed record UpdateBankRequest(
   long Id,
    string? Code,
    string? Name,
    string? WebSite,
    string? Phone,
    string UpdatedBy
);
