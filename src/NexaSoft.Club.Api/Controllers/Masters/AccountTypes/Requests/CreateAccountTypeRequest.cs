namespace NexaSoft.Club.Api.Controllers.Masters.AccountTypes.Request;

public sealed record CreateAccountTypeRequest(
    string? Name,
    string? Description,
    string CreatedBy
);
