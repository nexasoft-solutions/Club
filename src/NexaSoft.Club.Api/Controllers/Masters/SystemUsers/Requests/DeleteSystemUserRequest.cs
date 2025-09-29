namespace NexaSoft.Club.Api.Controllers.Masters.SystemUsers.Request;

public sealed record DeleteSystemUserRequest(
   long Id,
   string DeletedBy
);
