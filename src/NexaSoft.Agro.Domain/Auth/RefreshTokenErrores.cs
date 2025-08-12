using NexaSoft.Agro.Domain.Abstractions;

namespace NexaSoft.Agro.Domain.Auth;

public class RefreshTokenErrores
{
    public static readonly Error Invalido = new(
            "RefreshToken.Invalido",
            "El refresh token es inválido o ha expirado."
        );

    public static readonly Error RevokeFallido = new(
        "RefreshToken.RevokeFallido",
        "No se pudo revocar el token."
    );

    public static readonly Error NoEncontrado = new(
        "RefreshToken.NoEncontrado",
        "No se encontró el refresh token."
    );
}
