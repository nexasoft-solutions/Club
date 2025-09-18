using NexaSoft.Agro.Domain.Abstractions;
using static NexaSoft.Agro.Domain.Shareds.Enums;
using NexaSoft.Agro.Domain.Features.Proyectos.Responsables;
using NexaSoft.Agro.Domain.Features.Proyectos.EstudiosAmbientales;

namespace NexaSoft.Agro.Domain.Features.Proyectos.EventosRegulatorios;

public class EventoRegulatorio : Entity
{
    public string? NombreEvento { get; private set; }
    public int TipoEventoId { get; private set; }
    public int FrecuenciaEventoId { get; private set; }
    public DateOnly? FechaExpedición { get; private set; }
    public DateOnly? FechaVencimiento { get; private set; }
    public string? Descripcion { get; private set; }
    public int NotificarDíasAntes { get; private set; }
    public long ResponsableId { get; private set; }
    public Responsable? Responsable { get; private set; }
    public int EstadoEventoId { get; private set; }
    public long EstudioAmbientalId { get; private set; }
    public EstudioAmbiental? EstudioAmbiental { get; private set; }
    public int EstadoEventoRegulatorio { get; private set; }

    // Relación con múltiples responsables
    public List<EventoRegulatorioResponsable>? Responsables { get; private set; } = new();
    //public IReadOnlyCollection<EventoRegulatorioResponsable> Responsables => _responsables.AsReadOnly();


    private EventoRegulatorio() { }

    private EventoRegulatorio(
        string? nombreEvento,
        int tipoEventoId,
        int frecuenciaEventoId,
        DateOnly? fechaExpedición,
        DateOnly? fechaVencimiento,
        string? descripcion,
        int notificarDíasAntes,
        long responsableId,
        int estadoEventoId,
        long estudioAmbientalId,
        int estadoEventoRegulatorio,
        DateTime fechaCreacion,
        string? usuarioCreacion,
        string? usuarioModificacion = null,
        string? usuarioEliminacion = null
    //IEnumerable<long>? responsablesAdicionales = null // 👈 nuevo parámetro opcional
    ) : base(fechaCreacion, usuarioCreacion, usuarioModificacion, usuarioEliminacion)
    {
        NombreEvento = nombreEvento;
        TipoEventoId = tipoEventoId;
        FrecuenciaEventoId = frecuenciaEventoId;
        FechaExpedición = fechaExpedición;
        FechaVencimiento = fechaVencimiento;
        Descripcion = descripcion;
        NotificarDíasAntes = notificarDíasAntes;
        ResponsableId = responsableId;
        EstadoEventoId = estadoEventoId;
        EstudioAmbientalId = estudioAmbientalId;
        EstadoEventoRegulatorio = estadoEventoRegulatorio;
        FechaCreacion = fechaCreacion;
        UsuarioCreacion = usuarioCreacion;
        UsuarioModificacion = usuarioModificacion;
        UsuarioCreacion = usuarioCreacion;
        UsuarioModificacion = usuarioModificacion;
        UsuarioEliminacion = usuarioEliminacion;
    }

    public static EventoRegulatorio Create(
        string? nombreEvento,
        int tipoEventoId,
        int frecuenciaEventoId,
        DateOnly? fechaExpedición,
        DateOnly? fechaVencimiento,
        string? descripcion,
        int notificarDíasAntes,
        long responsableId,
        int estadoEventoId,
        long estudioAmbientalId,
        int estadoEventoRegulatorio,
        DateTime fechaCreacion,
        string? usuarioCreacion,
        IEnumerable<long>? responsablesAdicionales = null
    )
    {
        var entity = new EventoRegulatorio(
            nombreEvento,
            tipoEventoId,
            frecuenciaEventoId,
            fechaExpedición,
            fechaVencimiento,
            descripcion,
            notificarDíasAntes,
            responsableId,
            estadoEventoId,
            estudioAmbientalId,
            estadoEventoRegulatorio,
            fechaCreacion,
            usuarioCreacion
        );

        if (responsablesAdicionales != null)
        {
            entity.AddResponsables(responsablesAdicionales, fechaCreacion, usuarioCreacion!);
        }

        return entity;
    }

    public Result Update(
        long Id,
        string? nombreEvento,
        int tipoEventoId,
        int frecuenciaEventoId,
        DateOnly? fechaExpedición,
        DateOnly? fechaVencimiento,
        string? descripcion,
        int notificarDíasAntes,
        long responsableId,
        long estudioAmbientalId,
        DateTime utcNow,
        string? usuarioModificacion,
        IEnumerable<long>? responsablesAdicionales = null
    )
    {
        NombreEvento = nombreEvento;
        TipoEventoId = tipoEventoId;
        FrecuenciaEventoId = frecuenciaEventoId;
        FechaExpedición = fechaExpedición;
        FechaVencimiento = fechaVencimiento;
        Descripcion = descripcion;
        NotificarDíasAntes = notificarDíasAntes;
        ResponsableId = responsableId;
        EstudioAmbientalId = estudioAmbientalId;
        FechaModificacion = utcNow;
        UsuarioModificacion = usuarioModificacion;

        // Actualizar responsables
        Responsables ??= new List<EventoRegulatorioResponsable>();

        if (responsablesAdicionales is not null)
        {
            var actuales = Responsables.Select(r => r.ResponsableId).ToList();
            var paraEliminar = actuales.Except(responsablesAdicionales).ToList();
            var paraAgregar = responsablesAdicionales.Except(actuales).ToList();

            // Eliminar responsables
            Responsables.RemoveAll(r => paraEliminar.Contains(r.ResponsableId));

            // Agregar nuevos responsables si no existen
            foreach (var nuevoId in paraAgregar)
            {
                if (!Responsables.Any(r => r.ResponsableId == nuevoId && r.EventoRegulatorioId == this.Id))
                {
                    Responsables.Add(new EventoRegulatorioResponsable(this.Id, nuevoId, utcNow, usuarioModificacion!));
                }
            }
        }


        return Result.Success();
    }

    public Result UpdateStateEvent(DateTime utcNow, int nuevoEstado, string usuarioModificacion, DateOnly? fechaVencimiento = null)
    {
        if (nuevoEstado == (int)EstadosEventosEnum.Reprogramado)
            FechaVencimiento = fechaVencimiento;

        EstadoEventoId = nuevoEstado;
        UsuarioModificacion = usuarioModificacion;
        FechaModificacion = utcNow;

        return Result.Success();
    }

    public Result Delete(DateTime utcNow, string usuarioEliminacion)
    {
        EstadoEventoRegulatorio = (int)EstadosEnum.Eliminado;
        FechaEliminacion = utcNow;
        UsuarioEliminacion = usuarioEliminacion;
        return Result.Success();
    }

    public Result AddResponsables(IEnumerable<long> responsablesIds, DateTime utcNow, string usuarioCreacion)
    {
        foreach (var responsableId in responsablesIds)
        {
            if (!Responsables!.Any(r => r.ResponsableId == responsableId))
            {
                Responsables!.Add(new EventoRegulatorioResponsable(this.Id, responsableId, utcNow, usuarioCreacion));
            }
        }

        // RaiseDomainEvent(new ResponsablesAgregadosDomainEvent(this.Id, responsablesIds));

        return Result.Success();
    }
}
