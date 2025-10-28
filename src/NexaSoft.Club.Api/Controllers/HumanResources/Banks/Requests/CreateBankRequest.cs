namespace NexaSoft.Club.Api.Controllers.HumanResources.Banks.Request;

public sealed record CreateBankRequest(
    string? Code,
    string? Name,
    string? WebSite,
    string? Phone,
    string CreatedBy
);
