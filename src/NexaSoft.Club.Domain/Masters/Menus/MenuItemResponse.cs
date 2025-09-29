namespace NexaSoft.Club.Domain.Masters.Menus;

public record class MenuItemResponse
(
    string? Label,
    string? Icon,
    string? Route,
    List<MenuItemResponse> Items,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    string? CreatedBy,
    string? UpdatedBy
);
