namespace NexaSoft.Club.Api.Controllers.Masters.AccountTypes.Request;

public sealed record DeleteAccountTypeRequest(
   long Id,
   string DeletedBy
);
