using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.Features.Reservations;

public class ReservationErrores
{
    public static readonly Error NoEncontrado = new
    (
        "Reservation.NoEncontrado",
        "No se encontro Reservation"
    );

    public static readonly Error Duplicado = new
    (
        "Reservation.Duplicado",
        "Ya existe una Reservation con el mismo valor"
    );

    public static readonly Error ErrorSave = new
    (
        "Reservation.ErrorSave",
        "Error al guardar Reservation"
    );

    public static readonly Error ErrorEdit = new
    (
        "Reservation.ErrorEdit",
        "Error al editar Reservation"
    );

    public static readonly Error ErrorDelete = new
    (
        "Reservation.ErrorDelete",
        "Error al eliminar Reservation"
    );

    public static readonly Error ErrorConsulta = new
    (
        "Reservation.ErrorConsulta",
        "Error al listar Reservation"
    );
}
