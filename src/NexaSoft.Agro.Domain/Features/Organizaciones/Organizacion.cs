using NexaSoft.Agro.Domain.Abstractions;
using static NexaSoft.Agro.Domain.Shareds.Enums;

namespace NexaSoft.Agro.Domain.Features.Organizaciones;

public class Organizacion : Entity
{
    public string? NombreOrganizacion { get; private set; }
    public string? ContactoOrganizacion { get; private set; }
    public string? TelefonoContacto { get; private set; }
    public int SectorId { get; private set; }
    public string? RucOrganizacion { get; set; }
    public string? Observaciones { get; set; }
    public int EstadoOrganizacion { get; private set; }

    private Organizacion() { }

    private Organizacion(
        string? nombreOrganizacion,
        string? contactoOrganizacion,
        string? telefonoContacto,
        int sectorId,
        string? rucOrganizacion,
        string? observaciones,
        int estadoOrganizacion,
        DateTime fechaCreacion,
        string? usuarioCreacion,
        string? usuarioModificacion = null,
        string? usuarioEliminacion = null
    ) : base(fechaCreacion, usuarioCreacion, usuarioModificacion, usuarioEliminacion)
    {
        NombreOrganizacion = nombreOrganizacion;
        ContactoOrganizacion = contactoOrganizacion;
        TelefonoContacto = telefonoContacto;
        SectorId = sectorId;
        RucOrganizacion = rucOrganizacion;
        Observaciones = observaciones;
        EstadoOrganizacion = estadoOrganizacion;
        FechaCreacion = fechaCreacion;
        UsuarioCreacion = usuarioCreacion;
        UsuarioModificacion = usuarioModificacion;
        UsuarioEliminacion = usuarioEliminacion;
    }

    public static Organizacion Create(
        string? nombreOrganizacion, 
        string? contactoOrganizacion, 
        string? telefonoContacto, 
        int sectorId, 
        string? rucOrganizacion,
        string? observaciones, 
        int estadoOrganizacion, 
        DateTime fechaCreacion,
        string? usuarioCreacion
    )
    {
        var entity = new Organizacion(
            nombreOrganizacion,
            contactoOrganizacion,
            telefonoContacto,
            sectorId,
            rucOrganizacion,
            observaciones,
            estadoOrganizacion,
            fechaCreacion,
            usuarioCreacion
        );
        //entity.RaiseDomainEvent(new OrganizacionCreateDomainEvent(entity.Id));
        return entity;
    }

    public Result Update(
        long Id,
        string? nombreOrganizacion, 
        string? contactoOrganizacion, 
        string? telefonoContacto, 
        int sectorId, 
        string? rucOrganizacion,
        string? observaciones,
        DateTime utcNow,
        string? usuarioModificacion
    )
    {
        NombreOrganizacion = nombreOrganizacion;
        ContactoOrganizacion = contactoOrganizacion;
        TelefonoContacto = telefonoContacto;
        SectorId = sectorId;
        RucOrganizacion = rucOrganizacion;
        Observaciones = observaciones;
        FechaModificacion = utcNow;
        UsuarioModificacion = usuarioModificacion;

        //RaiseDomainEvent(new OrganizacionUpdateDomainEvent(this.Id));

        return Result.Success();
    }

    public Result Delete(DateTime utcNow, string usuarioEliminacion)
    {
        EstadoOrganizacion = (int)EstadosEnum.Eliminado;
        FechaEliminacion = utcNow;
        UsuarioEliminacion = usuarioEliminacion;
        return Result.Success();
    }
}
