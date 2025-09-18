using NexaSoft.Agro.Domain.Abstractions;
using static NexaSoft.Agro.Domain.Shareds.Enums;
using NexaSoft.Agro.Domain.Masters.Ubigeos;


namespace NexaSoft.Agro.Domain.Features.Organizaciones.Empresas;

public class Empresa : Entity
{
    public string? RazonSocial { get; private set; }
    public string? RucEmpresa { get; private set; }
    public string? ContactoEmpresa { get; private set; }
    public string? TelefonoContactoEmpresa { get; private set; }
    public long DepartamentoEmpresaId { get; private set; }
    public Ubigeo? DepartamentoEmpresa { get; private set; }
    public long ProvinciaEmpresaId { get; private set; }
    public Ubigeo? ProvinciaEmpresa { get; private set; }
    public long DistritoEmpresaId { get; private set; }
    public Ubigeo? DistritoEmpresa { get; private set; }
    public string? Direccion { get; private set; }
    public double LatitudEmpresa { get; private set; }
    public double LongitudEmpresa { get; private set; }
    public long OrganizacionId { get; private set; }
    public int EstadoEmpresa { get; private set; }
    public Organizacion? Organizacion { get; private set; }

    private Empresa() { }

    private Empresa(
        string? razonSocial,
        string? rucEmpresa,
        string? contactoEmpresa,
        string? telefonoContactoEmpresa,
        long departamentoEmpresaId,
        long provinciaEmpresaId,
        long distritoEmpresaId,
        string? direccion,
        double latitudEmpresa,
        double longitudEmpresa,
        long organizacionId,
        int estadoEmpresa,
        DateTime fechaCreacion,
        string? usuarioCreacion,
        string? usuarioModificacion = null,
        string? usuarioEliminacion = null
    ) : base(fechaCreacion, usuarioCreacion, usuarioModificacion, usuarioEliminacion)
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
        UsuarioCreacion = usuarioCreacion;
        UsuarioModificacion = usuarioModificacion;
        UsuarioEliminacion = usuarioEliminacion;
    }

    public static Empresa Create(
        string? razonSocial, 
        string? rucEmpresa, 
        string? contactoEmpresa, 
        string? telefonoContactoEmpresa, 
        long departamentoEmpresaId, 
        long provinciaEmpresaId, 
        long distritoEmpresaId, 
        string? direccion, 
        double latitudEmpresa, 
        double longitudEmpresa, 
        long organizacionId, 
        int estadoEmpresa, 
        DateTime fechaCreacion,
        string? usuarioCreacion
    )
    {
        var entity = new Empresa(
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
            fechaCreacion,
            usuarioCreacion
        );
        //entity.RaiseDomainEvent(new EmpresaCreateDomainEvent(entity.Id));
        return entity;
    }

    public Result Update(
        long Id,
        string? razonSocial, 
        string? rucEmpresa, 
        string? contactoEmpresa, 
        string? telefonoContactoEmpresa, 
        long departamentoEmpresaId, 
        long provinciaEmpresaId, 
        long distritoEmpresaId, 
        string? direccion, 
        double latitudEmpresa, 
        double longitudEmpresa, 
        long organizacionId, 
        DateTime utcNow,
        string? usuarioModificacion
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
        UsuarioModificacion = usuarioModificacion;

        //RaiseDomainEvent(new EmpresaUpdateDomainEvent(this.Id));

        return Result.Success();
    }

    public Result Delete(DateTime utcNow, string? usuarioEliminacion)
    {
        EstadoEmpresa = (int)EstadosEnum.Eliminado;
        FechaEliminacion = utcNow;
        UsuarioEliminacion = usuarioEliminacion;
        return Result.Success();
    }
}
