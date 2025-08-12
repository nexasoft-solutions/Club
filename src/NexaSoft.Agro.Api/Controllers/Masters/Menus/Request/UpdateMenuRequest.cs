namespace NexaSoft.Agro.Api.Controllers.Masters.Menus.Request;

public sealed record UpdateMenuRequest
(
    Guid Id,
    string? Label,
    string? Icon,
    string? Route   
);