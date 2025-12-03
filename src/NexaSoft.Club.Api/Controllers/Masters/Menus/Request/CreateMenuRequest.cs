namespace NexaSoft.Club.Api.Controllers.Masters.Menus.Request;

public sealed record CreateMenuRequest
(
    string? Label,
    string? Icon,
    string? Route,
    long? ParentId,
    string? CreatedBy
);

