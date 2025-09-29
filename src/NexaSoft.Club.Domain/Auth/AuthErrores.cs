
using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.Auth;

public static class AuthErrores
{
    public static readonly Error LoginError = new(
        "Auth.LoginError",
        "Error al iniciar sesión."
    );

    public static readonly Error RefreshTokenError = new(
        "Auth.RefreshTokenError",
        "Error al refrescar el token."
    );

    public static readonly Error TokenGeneracion = new(
        "Auth.TokenGeneracion",
        "Error al generar el token de acceso."
    );

    public static readonly Error UsuarioInvalido = new(
        "Auth.UsuarioInvalido",
        "El usuario no es válido."
    );
}
