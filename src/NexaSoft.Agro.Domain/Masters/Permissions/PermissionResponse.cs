namespace NexaSoft.Agro.Domain.Masters.Permissions;

public record class PermissionResponse
(
    Guid id,
    string? name,
    string? description,
    string? referenciaControl,
    DateTime fechaCreacion
);