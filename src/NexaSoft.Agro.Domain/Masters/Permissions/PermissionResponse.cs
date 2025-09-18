namespace NexaSoft.Agro.Domain.Masters.Permissions;

public record class PermissionResponse
(
    long id,
    string? name,
    string? description,
    string? referenciaControl,
    DateTime fechaCreacion,
    DateTime? FechaModificacion,
    string? UsuarioCreacion,
    string? UsuarioModificacion

);