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

    public static readonly Error TarifaNoValida = new(
        "Reservation.TarifaNoValida",
        "La tarifa del espacio no es válida o está inactiva");
    public static readonly Error DisponibilidadNoValida = new(
        "Reservation.DisponibilidadNoValida",
        "La disponibilidad no es válida o no pertenece al espacio");
    public static readonly Error HorarioNoCompatible = new(
        "Reservation.HorarioNoCompatible",
        "El horario no es compatible con la disponibilidad");
    public static readonly Error NoDisponible = new(
        "Reservation.NoDisponible",
        "El espacio no está disponible en el horario solicitado");
    public static readonly Error TiempoExcedido = new(
        "Reservation.TiempoExcedido",
        "El tiempo de reserva excede el máximo permitido");
}
