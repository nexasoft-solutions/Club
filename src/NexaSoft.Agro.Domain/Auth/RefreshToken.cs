using NexaSoft.Agro.Domain.Abstractions;

namespace NexaSoft.Agro.Domain.Auth;

public class RefreshToken : Entity
{
    public string? Token { get; private set; }
    public long UserId { get; private set; }
    public DateTime Expires { get; private set; }
    public bool Revoked { get; private set; }

    private RefreshToken() { }

    private RefreshToken(
        string token,
        long userId,
        DateTime expires,
        DateTime fechaCreacion,
        string usuarioCreacion
    ) : base(fechaCreacion, usuarioCreacion)
    {
        Token = token;
        UserId = userId;
        Expires = expires;
        Revoked = false;
        FechaCreacion = fechaCreacion;
        UsuarioCreacion = usuarioCreacion;
    }

    public static RefreshToken Create(
        string token,
        long userId,
        DateTime expires,
        string usuarioCreacion
    )
    {
        return new RefreshToken(
            token,
            userId,
            expires,
            DateTime.UtcNow,
            usuarioCreacion
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