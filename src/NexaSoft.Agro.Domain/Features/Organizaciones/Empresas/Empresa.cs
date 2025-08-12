using NexaSoft.Agro.Domain.Abstractions;
using NexaSoft.Agro.Domain.Features.Organizaciones.Empresas.Events;
using static NexaSoft.Agro.Domain.Shareds.Enums;
using NexaSoft.Agro.Domain.Masters.Ubigeos;


namespace NexaSoft.Agro.Domain.Features.Organizaciones.Empresas;

public class Empresa : Entity
{
    public string? RazonSocial { get; private set; }
    public string? RucEmpresa { get; private set; }
    public string? ContactoEmpresa { get; private set; }
    public string? TelefonoContactoEmpresa { get; private set; }
    public Guid DepartamentoEmpresaId { get; private set; }
    public Ubigeo? DepartamentoEmpresa { get; private set; }
    public Guid ProvinciaEmpresaId { get; private set; }
    public Ubigeo? ProvinciaEmpresa { get; private set; }
    public Guid DistritoEmpresaId { get; private set; }
    public Ubigeo? DistritoEmpresa { get; private set; }
    public string? Direccion { get; private set; }
    public double LatitudEmpresa { get; private set; }
    public double LongitudEmpresa { get; private set; }
    public Guid OrganizacionId { get; private set; }
    public int EstadoEmpresa { get; private set; }
    public Organizacion? Organizacion { get; private set; }

    private Empresa() { }

    private Empresa(
        Guid id, 
        string? razonSocial, 
        string? rucEmpresa, 
        string? contactoEmpresa, 
        string? telefonoContactoEmpresa, 
        Guid departamentoEmpresaId, 
        Guid provinciaEmpresaId, 
        Guid distritoEmpresaId, 
        string? direccion, 
        double latitudEmpresa, 
        double longitudEmpresa, 
        Guid organizacionId, 
        int estadoEmpresa, 
        DateTime fechaCreacion
    ) : base(id, fechaCreacion)
    {
        RazonSocial = razonSocial;
        RucEmpresa = rucEmpresa;
        ContactoEmpresa = contactoEmpresa;
        TelefonoContactoEmpresa = telefonoContactoEmpresa;
        DepartamentoEmpresaId = departamentoEmpresaId;
        ProvinciaEmpresaId = provinciaEmpresaId;
        DistritoEmpresaId = distritoEmpresaId;
        Direccion = direccion;
        LatitudEmpresa = latitudEmpresa;
        LongitudEmpresa = longitudEmpresa;
        OrganizacionId = organizacionId;
        EstadoEmpresa = estadoEmpresa;
        FechaCreacion = fechaCreacion;
    }

    public static Empresa Create(
        string? razonSocial, 
        string? rucEmpresa, 
        string? contactoEmpresa, 
        string? telefonoContactoEmpresa, 
        Guid departamentoEmpresaId, 
        Guid provinciaEmpresaId, 
        Guid distritoEmpresaId, 
        string? direccion, 
        double latitudEmpresa, 
        double longitudEmpresa, 
        Guid organizacionId, 
        int estadoEmpresa, 
        DateTime fechaCreacion
    )
    {
        var entity = new Empresa(
            Guid.NewGuid(),
            razonSocial,
            rucEmpresa,
            contactoEmpresa,
            telefonoContactoEmpresa,
            departamentoEmpresaId,
            provinciaEmpresaId,
            distritoEmpresaId,
            direccion,
            latitudEmpresa,
            longitudEmpresa,
            organizacionId,
            estadoEmpresa,
            fechaCreacion
        );
        entity.RaiseDomainEvent(new EmpresaCreateDomainEvent(entity.Id));
        return entity;
    }

    public Result Update(
        Guid Id,
        string? razonSocial, 
        string? rucEmpresa, 
        string? contactoEmpresa, 
        string? telefonoContactoEmpresa, 
        Guid departamentoEmpresaId, 
        Guid provinciaEmpresaId, 
        Guid distritoEmpresaId, 
        string? direccion, 
        double latitudEmpresa, 
        double longitudEmpresa, 
        Guid organizacionId, 
        DateTime utcNow
    )
    {
        RazonSocial = razonSocial;
        RucEmpresa = rucEmpresa;
        ContactoEmpresa = contactoEmpresa;
        TelefonoContactoEmpresa = telefonoContactoEmpresa;
        DepartamentoEmpresaId = departamentoEmpresaId;
        ProvinciaEmpresaId = provinciaEmpresaId;
        DistritoEmpresaId = distritoEmpresaId;
        Direccion = direccion;
        LatitudEmpresa = latitudEmpresa;
        LongitudEmpresa = longitudEmpresa;
        OrganizacionId = organizacionId;
        FechaModificacion = utcNow;

        RaiseDomainEvent(new EmpresaUpdateDomainEvent(this.Id));

        return Result.Success();
    }

    public Result Delete(DateTime utcNow)
    {
        EstadoEmpresa = (int)EstadosEnum.Eliminado;
        FechaEliminacion = utcNow;
        return Result.Success();
    }
}
