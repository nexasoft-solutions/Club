namespace NexaSoft.Club.Api.Controllers.Masters.UserTypes.Request;

public sealed record DeleteUserTypeRequest(
   long Id,
   string DeletedBy
);
