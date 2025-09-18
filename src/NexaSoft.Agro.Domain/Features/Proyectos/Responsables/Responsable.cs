using NexaSoft.Agro.Domain.Abstractions;
using NexaSoft.Agro.Domain.Features.Proyectos.EstudiosAmbientales;
using static NexaSoft.Agro.Domain.Shareds.Enums;

namespace NexaSoft.Agro.Domain.Features.Proyectos.Responsables;

public class Responsable : Entity
{
    public string? NombreResponsable { get; private set; }
    public string? CargoResponsable { get; private set; }
    public string? CorreoResponsable { get; private set; }
    public string? TelefonoResponsable { get; private set; }
    public string? Observaciones { get; private set; }
    public int EstadoResponsable { get; private set; }

    public long? EstudioAmbientalId { get; private set; }
    public EstudioAmbiental? EstudioAmbiental { get; private set; }


    private Responsable() { }

    private Responsable(
        string? nombreResponsable,
        string? cargoResponsable,
        string? correoResponsable,
        string? telefonoResponsable,
        string? observaciones,
        long? estudioAmbientalId,
        int estadoResponsable,
        DateTime fechaCreacion,
        string? usuarioCreacion,        
        string? usuarioModificacion = null,
        string? usuarioEliminacion = null
    ) : base(fechaCreacion, usuarioCreacion, usuarioModificacion, usuarioEliminacion)
    {
        NombreResponsable = nombreResponsable;
        CargoResponsable = cargoResponsable;
        CorreoResponsable = correoResponsable;
        TelefonoResponsable = telefonoResponsable;
        Observaciones = observaciones;
        EstudioAmbientalId = estudioAmbientalId;
        EstadoResponsable = estadoResponsable;
        FechaCreacion = fechaCreacion;
        UsuarioCreacion = usuarioCreacion;
        UsuarioModificacion = usuarioModificacion;
        UsuarioEliminacion = usuarioEliminacion;
    }

    public static Responsable Create(
        string? nombreResponsable, 
        string? cargoResponsable, 
        string? correoResponsable, 
        string? telefonoResponsable, 
        string? observaciones, 
        long? estudioAmbientalId,
        int estadoResponsable, 
        DateTime fechaCreacion,
        string? usuarioCreacion
    )
    {
        var entity = new Responsable(
            nombreResponsable,
            cargoResponsable,
            correoResponsable,
            telefonoResponsable,
            observaciones,
            estudioAmbientalId,
            estadoResponsable,
            fechaCreacion,
            usuarioCreacion
        );
        //entity.RaiseDomainEvent(new ResponsableCreateDomainEvent(entity.Id));
        return entity;
    }

    public Result Update(
        long Id,
        string? nombreResponsable, 
        string? cargoResponsable, 
        string? correoResponsable, 
        string? telefonoResponsable, 
        string? observaciones, 
        DateTime utcNow,
        string? usuarioModificacion
    )
    {
        NombreResponsable = nombreResponsable;
        CargoResponsable = cargoResponsable;
        CorreoResponsable = correoResponsable;
        TelefonoResponsable = telefonoResponsable;
        Observaciones = observaciones;
        FechaModificacion = utcNow;
        UsuarioModificacion = usuarioModificacion;
        return Result.Success();
    }

    public Result Delete(DateTime utcNow, string usuarioEliminacion)
    {
        EstadoResponsable = (int)EstadosEnum.Eliminado;
        FechaEliminacion = utcNow;
        UsuarioEliminacion = usuarioEliminacion;
        return Result.Success();
    }
}
