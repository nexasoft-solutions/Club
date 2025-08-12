namespace NexaSoft.Agro.Domain.Masters.Users;

public sealed record RoleDefultResponse
(
    Guid RoleId,
    bool? IsDefault
);
