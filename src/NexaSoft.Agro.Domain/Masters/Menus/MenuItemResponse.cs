namespace NexaSoft.Agro.Domain.Masters.Menus;

public record class MenuItemResponse
(
    string? Label,
    string? Icon,
    string? Route,
    List<MenuItemResponse> Items
);
