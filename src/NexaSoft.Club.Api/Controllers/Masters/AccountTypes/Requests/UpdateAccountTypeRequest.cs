namespace NexaSoft.Club.Api.Controllers.Masters.AccountTypes.Request;

public sealed record UpdateAccountTypeRequest(
   long Id,
    string? Name,
    string? Description,
    string UpdatedBy
);
