using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.Features.Members;

public class MemberErrores
{
    public static readonly Error NoEncontrado = new
    (
        "Member.NoEncontrado",
        "No se encontro Member"
    );

    public static readonly Error Duplicado = new
    (
        "Member.Duplicado",
        "Ya existe una Member con el mismo valor"
    );

    public static readonly Error ErrorSave = new
    (
        "Member.ErrorSave",
        "Error al guardar Member"
    );

    public static readonly Error ErrorEdit = new
    (
        "Member.ErrorEdit",
        "Error al editar Member"
    );

    public static readonly Error ErrorDelete = new
    (
        "Member.ErrorDelete",
        "Error al eliminar Member"
    );

    public static readonly Error ErrorConsulta = new
    (
        "Member.ErrorConsulta",
        "Error al listar Member"
    );

    public static readonly Error TipoNoExiste = new
    (
        "Member.TipoNoExiste",
        "El tipo de menbresia no existe"
    );

    public static readonly Error NotActive = new
    (
        "Member.NotActive",
        "Member no está activo"
    );

    public static readonly Error PasswordErrado = new
    (
        "Member.PasswordErrado",
        "El password debe tener por lo menos 6 caracteres"
    );

    public static readonly Error PinInvalido = new
    (
        "Member.PinInvalido",
        "PIN inválido o expirado"
    );

    public static readonly Error ErrorValidandoPin = new
    (
        "Member.ErrorValidandoPin",
        "Error validando PIN"
    );

    public static readonly Error ErrorGenerarToken = new
    (
        "Member.ErrorGenerarToken",
        "Error generando token"
    );

    public static readonly Error PasswordInvalido = new
    (
        "Member.PasswordInvalido",
        "Credenciales inválidas"
    );

    public static readonly Error RefreshTokenInvalido = new
    (
        "Member.RefreshTokenInvalido",
        "Refresh token inválido"
    );

    public static readonly Error ErrorRefreshToken = new
   (
       "Member.ErrorRefreshToken",
       "Error refrescando token"
   );

    public static readonly Error ErrorConfigPassword = new
   (
       "Member.ErrorConfigPassword",
       "Debe configurar su password primero"
   );

    public static readonly Error ErrorLogin = new
    (
        "Member.ErrorLogin",
        "Error en el login"
    );

    public static readonly Error ErrorDatosAdicionales = new
    (
        "Member.ErrorDatosAdicionales",
        "Error obteniendo datos del member"
    );
    public static readonly Error QrInvalidoOExpirado = new
    (
        "Member.QrInvalidoOExpirado",
        "QR code inválido o expirado"
    );

    public static readonly Error YaTieneVisitaActiva = new
    (
        "Member.YaTieneVisitaActiva",
        "El member ya tiene una visita activa"
    );

    public static readonly Error ErrorDataMetrics = new
    (
        "Member.ErrorDataMetrics",
        "Error al obtener metricas del member"
    );

    public static readonly Error ErrorHasPassword = new
    (
        "Member.ErrorHasPassword",
        "Miembro no tiene password configurado"
    );

    public static readonly Error ErrorQrNoGenerado = new
    (
        "Member.ErrorQrNoGenerado",
        "El QR no ha sido generado"
    );
}
