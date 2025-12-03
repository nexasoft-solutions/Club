namespace NexaSoft.Club.Api.Controllers.Masters.Menus.Request;

public sealed record DeleteMenuRequest(
    long Id,
    string DeletedBy 
);

