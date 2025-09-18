namespace NexaSoft.Agro.Domain.Masters.Menus;

public sealed record MenuResponse
(
    long Id,
    string? Label,
    string? Icon,
    string? Route,
    long? ParentId
);