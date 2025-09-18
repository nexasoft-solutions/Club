namespace NexaSoft.Agro.Domain.Features.Proyectos.Responsables;

public class EventoRegulatorioResponsable
{
    public long EventoRegulatorioId { get; private set; }
    public long ResponsableId { get; private set; }

    public DateTime FechaCreacion { get; private set; }
    public string? UsuarioCreacion { get; private set; }

    public EventoRegulatorioResponsable()
    {

    }
    public EventoRegulatorioResponsable(long eventoRegulatorioId, long responsableId, DateTime fechaCreacion, string? usuarioCreacion)
    {
        EventoRegulatorioId = eventoRegulatorioId;
        ResponsableId = responsableId;
        FechaCreacion = fechaCreacion;
        UsuarioCreacion = usuarioCreacion;
    }
}
