namespace NexaSoft.Club.Domain.Masters.Menus;

public sealed record MenuResponse
(
    long Id,
    string? Label,
    string? Icon,
    string? Route,
    long? ParentId,
    List<MenuResponse>? Children = null
);

public sealed record MenuItemsResponse
(
    long Id,
    string? Label,
    string? Icon,
    string? Route,
    long? ParentId
);