using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.Features.Payments;

public class PaymentErrores
{
    public static readonly Error NoEncontrado = new
    (
        "Payment.NoEncontrado",
        "No se encontro Payment"
    );

    public static readonly Error Duplicado = new
    (
        "Payment.Duplicado",
        "Ya existe una Payment con el mismo valor"
    );

    public static readonly Error ErrorSave = new
    (
        "Payment.ErrorSave",
        "Error al guardar Payment"
    );

    public static readonly Error ErrorEdit = new
    (
        "Payment.ErrorEdit",
        "Error al editar Payment"
    );

    public static readonly Error ErrorDelete = new
    (
        "Payment.ErrorDelete",
        "Error al eliminar Payment"
    );

    public static readonly Error ErrorConsulta = new
    (
        "Payment.ErrorConsulta",
        "Error al listar Payment"
    );

    public static readonly Error MiembroNoExiste = new
    (
        "Payment.MiembroNoExiste",
        "Miembro no existe"
    );

    public static readonly Error CuotaNoExiste = new
    (
        "Payment.CuotaNoExiste",
        "Cuota indicada no existe"
    );

    public static readonly Error CuotaYaPagada = new
    (
        "Payment.CuotaYaPagada",
        "La Cuota ya se encuentra pagada"
    );

    public static readonly Error MontoNoCoincide = new
    (
        "Payment.MontoNoCoincide",
        "El monto no coincide con la suma de items"
    );

    public static readonly Error CuotaInvalida = new
    (
        "Payment.CuotaInvalida",
        "Cuota Invalida"
    );

    public static readonly Error MontoExcedeDeuda = new
    (
        "Payment.MontoExcedeDeuda",
        "El monto ingresado excede la deuda"
    );
    
    
    public static readonly Error MontoMayorCero = new
    (
        "Payment.MontoMayorCero",
        "El monto debe ser mayor a cero"
    );
    
    
}
