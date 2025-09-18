using NexaSoft.Agro.Domain.Abstractions;
using static NexaSoft.Agro.Domain.Shareds.Enums;
using NexaSoft.Agro.Domain.Features.Proyectos.EventosRegulatorios;

namespace NexaSoft.Agro.Domain.Features.Proyectos.Cumplimientos;

public class Cumplimiento : Entity
{
    public DateOnly? FechaCumplimiento { get; private set; }
    public bool? RegistradoaTiempo { get; private set; }
    public string? Observaciones { get; private set; }
    public long EventoRegulatorioId { get; private set; }
    public EventoRegulatorio? EventoRegulatorio { get; private set; }
    public int EstadoCumplimiento { get; private set; }

    private Cumplimiento() { }

    private Cumplimiento(
        DateOnly? fechaCumplimiento,
        bool? registradoaTiempo,
        string? observaciones,
        long eventoRegulatorioId,
        int estadoCumplimiento,
        DateTime fechaCreacion,
        string? usuarioCreacion,
        string? usuarioModificacion = null,
        string? usuarioEliminacion = null
    ) : base(fechaCreacion, usuarioCreacion, usuarioModificacion, usuarioEliminacion)
    {
        FechaCumplimiento = fechaCumplimiento;
        RegistradoaTiempo = registradoaTiempo;
        Observaciones = observaciones;
        EventoRegulatorioId = eventoRegulatorioId;
        EstadoCumplimiento = estadoCumplimiento;
        FechaCreacion = fechaCreacion;
        UsuarioCreacion = usuarioCreacion;
        UsuarioModificacion = usuarioModificacion;
        UsuarioEliminacion = usuarioEliminacion;
    }

    public static Cumplimiento Create(
        DateOnly? fechaCumplimiento,
        bool? registradoaTiempo,
        string? observaciones,
        long eventoRegulatorioId,
        int estadoCumplimiento,
        DateTime fechaCreacion,
        string? usuarioCreacion
    )
    {
        var entity = new Cumplimiento(
            fechaCumplimiento,
            registradoaTiempo,
            observaciones,
            eventoRegulatorioId,
            estadoCumplimiento,
            fechaCreacion,
            usuarioCreacion
        );
        //entity.RaiseDomainEvent(new CumplimientoCreateDomainEvent(entity.Id));
        return entity;
    }

    public Result Update(
        long Id,
        DateOnly? fechaCumplimiento,
        bool? registradoaTiempo,
        string? observaciones,
        long eventoRegulatorioId,
        DateTime utcNow,
        string? usuarioModificacion
    )
    {
        FechaCumplimiento = fechaCumplimiento;
        RegistradoaTiempo = registradoaTiempo;
        Observaciones = observaciones;
        EventoRegulatorioId = eventoRegulatorioId;
        FechaModificacion = utcNow;
        UsuarioModificacion = usuarioModificacion;

        //RaiseDomainEvent(new CumplimientoUpdateDomainEvent(this.Id));

        return Result.Success();
    }

    public Result Delete(DateTime utcNow,string usuarioEliminacion)
    {
        EstadoCumplimiento = (int)EstadosEnum.Eliminado;
        FechaEliminacion = utcNow;
        UsuarioEliminacion = usuarioEliminacion;
        return Result.Success();
    }
}
