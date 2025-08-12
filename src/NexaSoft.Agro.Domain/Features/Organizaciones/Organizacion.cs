using NexaSoft.Agro.Domain.Abstractions;
using NexaSoft.Agro.Domain.Features.Organizaciones.Events;
using static NexaSoft.Agro.Domain.Shareds.Enums;

namespace NexaSoft.Agro.Domain.Features.Organizaciones;

public class Organizacion : Entity
{
    public string? NombreOrganizacion { get; private set; }
    public string? ContactoOrganizacion { get; private set; }
    public string? TelefonoContacto { get; private set; }
    public int SectorId { get; private set; }
    public int EstadoOrganizacion { get; private set; }

    private Organizacion() { }

    private Organizacion(
        Guid id, 
        string? nombreOrganizacion, 
        string? contactoOrganizacion, 
        string? telefonoContacto, 
        int sectorId, 
        int estadoOrganizacion, 
        DateTime fechaCreacion
    ) : base(id, fechaCreacion)
    {
        NombreOrganizacion = nombreOrganizacion;
        ContactoOrganizacion = contactoOrganizacion;
        TelefonoContacto = telefonoContacto;
        SectorId = sectorId;
        EstadoOrganizacion = estadoOrganizacion;
        FechaCreacion = fechaCreacion;
    }

    public static Organizacion Create(
        string? nombreOrganizacion, 
        string? contactoOrganizacion, 
        string? telefonoContacto, 
        int sectorId, 
        int estadoOrganizacion, 
        DateTime fechaCreacion
    )
    {
        var entity = new Organizacion(
            Guid.NewGuid(),
            nombreOrganizacion,
            contactoOrganizacion,
            telefonoContacto,
            sectorId,
            estadoOrganizacion,
            fechaCreacion
        );
        entity.RaiseDomainEvent(new OrganizacionCreateDomainEvent(entity.Id));
        return entity;
    }

    public Result Update(
        Guid Id,
        string? nombreOrganizacion, 
        string? contactoOrganizacion, 
        string? telefonoContacto, 
        int sectorId, 
        DateTime utcNow
    )
    {
        NombreOrganizacion = nombreOrganizacion;
        ContactoOrganizacion = contactoOrganizacion;
        TelefonoContacto = telefonoContacto;
        SectorId = sectorId;
        FechaModificacion = utcNow;

        RaiseDomainEvent(new OrganizacionUpdateDomainEvent(this.Id));

        return Result.Success();
    }

    public Result Delete(DateTime utcNow)
    {
        EstadoOrganizacion = (int)EstadosEnum.Eliminado;
        FechaEliminacion = utcNow;
        return Result.Success();
    }
}
