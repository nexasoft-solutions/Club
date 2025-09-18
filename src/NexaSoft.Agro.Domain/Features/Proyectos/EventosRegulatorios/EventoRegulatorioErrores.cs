using NexaSoft.Agro.Domain.Abstractions;

namespace NexaSoft.Agro.Domain.Features.Proyectos.EventosRegulatorios;

public class EventoRegulatorioErrores
{
    public static readonly Error NoEncontrado = new
    (
        "EventoRegulatorio.NoEncontrado",
        "No se encontro EventoRegulatorio"
    );

    public static readonly Error Duplicado = new
    (
        "EventoRegulatorio.Duplicado",
        "Ya existe una EventoRegulatorio con el mismo valor"
    );

    public static readonly Error ErrorSave = new
    (
        "EventoRegulatorio.ErrorSave",
        "Error al guardar EventoRegulatorio"
    );

    public static readonly Error ErrorEdit = new
    (
        "EventoRegulatorio.ErrorEdit",
        "Error al editar EventoRegulatorio"
    );

    public static readonly Error ErrorDelete = new
    (
        "EventoRegulatorio.ErrorDelete",
        "Error al eliminar EventoRegulatorio"
    );

    public static readonly Error ErrorConsulta = new
    (
        "EventoRegulatorio.ErrorConsulta",
        "Error al listar EventoRegulatorio"
    );

    public static readonly Error ErrorAgregarResponsables = new
    (
        "EventoRegulatorio.ErrorAgregarResponsables",
        "Error al agregar responsables al evento"
    );

    public static readonly Error ErrorEstadoEvento = new
    (
        "EventoRegulatorio.ErrorEstadoEvento",
        "El evento tiene el mismo estado"
    );

    public static readonly Error ErrorFaltaFechaReprogramacion = new
    (
        "EventoRegulatorio.ErrorFaltaFechaReprogramacion",
        "El evento tiene el mismo estado"
    );

    public static readonly Error ErrorFlujoTerminado = new
    (
        "EventoRegulatorio.ErrorFlujoTerminado",
        "No se puede modificar un evento que ya ha sido presentado o cancelado."
    );

    
}
