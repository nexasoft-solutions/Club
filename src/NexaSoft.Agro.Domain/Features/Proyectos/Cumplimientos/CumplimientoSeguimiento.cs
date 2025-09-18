using NexaSoft.Agro.Domain.Abstractions;
using NexaSoft.Agro.Domain.Features.Proyectos.EventosRegulatorios;

namespace NexaSoft.Agro.Domain.Features.Proyectos.Cumplimientos;

public class CumplimientoSeguimiento : Entity
{
    public long EventoRegulatorioId { get; private set; }
    public EventoRegulatorio? EventoRegulatorio { get; private set; }
    public int? EstadoAnteriorId { get; private set; }
    public int EstadoNuevoId { get; private set; }

    public string? Observaciones { get; private set; }

    public DateOnly FechaCambio { get; private set; }

    private CumplimientoSeguimiento() { }

    private CumplimientoSeguimiento(
        long eventoRegulatorioId,
        int? estadoAnteriorId,
        int estadoNuevoId,
        string? observaciones,
        DateOnly fechaCambio,
        DateTime fechaCreacion,
        string usuarioCreacion
    ) : base(fechaCreacion, usuarioCreacion)
    {
        EventoRegulatorioId = eventoRegulatorioId;
        EstadoAnteriorId = estadoAnteriorId;
        EstadoNuevoId = estadoNuevoId;
        Observaciones = observaciones;
        FechaCambio = fechaCambio;
        FechaCreacion = fechaCreacion;
        UsuarioCreacion = usuarioCreacion;
    }

    public static CumplimientoSeguimiento Create(
        long eventoRegulatorioId,
        int? estadoAnteriorId,
        int estadoNuevoId,
        string? observaciones,
        DateOnly fechaCambio,
        DateTime fechaCreacion,
        string? usuarioCreacion
    )
    {
        return new CumplimientoSeguimiento(
            eventoRegulatorioId,
            estadoAnteriorId,
            estadoNuevoId,
            observaciones,
            fechaCambio,
            fechaCreacion,
            usuarioCreacion!
        );
    }
}