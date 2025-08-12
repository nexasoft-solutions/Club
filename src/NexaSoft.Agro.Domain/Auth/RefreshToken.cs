using NexaSoft.Agro.Domain.Abstractions;

namespace NexaSoft.Agro.Domain.Auth;

public class RefreshToken : Entity
{
    public string? Token { get; private set; }
    public Guid UserId { get; private set; }
    public DateTime Expires { get; private set; }
    public bool Revoked { get; private set; }

    private RefreshToken() { }

    private RefreshToken(
        Guid id,
        string token,
        Guid userId,
        DateTime expires,
        DateTime fechaCreacion
    ) : base(id, fechaCreacion)
    {
        Token = token;
        UserId = userId;
        Expires = expires;
        Revoked = false;
        FechaCreacion = fechaCreacion;
    }

    public static RefreshToken Create(string token, Guid userId, DateTime expires)
    {
        return new RefreshToken(
            Guid.NewGuid(),
            token,
            userId,
            expires,
            DateTime.UtcNow
        );
    }

    public void Revoke()
    {
        Revoked = true;
    }

    public bool IsActive()
    {
        return !Revoked && DateTime.UtcNow < Expires;
    }
}