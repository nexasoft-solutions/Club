namespace NexaSoft.Agro.Domain.Masters.Menus;

public sealed record MenuResponse
(
    Guid Id,
    string? Label,
    string? Icon,
    string? Route,
    Guid? ParentId
);