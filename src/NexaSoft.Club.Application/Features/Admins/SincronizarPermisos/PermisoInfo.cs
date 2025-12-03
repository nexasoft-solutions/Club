namespace NexaSoft.Club.Application.Features.Admins.SincronizarPermisos;

public sealed record PermisoInfo
(
    string? Name,
    string? Description,
    string? Reference,
    string? Source,
    string? Action
);