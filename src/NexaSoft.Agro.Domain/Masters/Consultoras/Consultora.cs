using NexaSoft.Agro.Domain.Abstractions;
using static NexaSoft.Agro.Domain.Shareds.Enums;

namespace NexaSoft.Agro.Domain.Masters.Consultoras;

public class Consultora : Entity
{
    public string? NombreConsultora { get; private set; }
    public string? DireccionConsultora { get; private set; }
    public string? RepresentanteConsultora { get; private set; }
    public string? RucConsultora { get; private set; }
    public string? CorreoOrganizacional { get; private set; }
    public int EstadoConsultora { get; private set; }

    private Consultora() { }

    private Consultora(
        string? nombreConsultora,
        string? direccionConsultora,
        string? representanteConsultora,
        string? rucConsultora,
        string? correoOrganizacional,
        int estadoConsultora,
        DateTime fechaCreacion,
        string? usuarioCreacion,
        string? usuarioModificacion = null,
        string? usuarioEliminacion = null
    ) : base(fechaCreacion, usuarioCreacion, usuarioModificacion, usuarioEliminacion)
    {
        NombreConsultora = nombreConsultora;
        DireccionConsultora = direccionConsultora;
        RepresentanteConsultora = representanteConsultora;
        RucConsultora = rucConsultora;
        CorreoOrganizacional = correoOrganizacional;
        EstadoConsultora = estadoConsultora;
        FechaCreacion = fechaCreacion;
        UsuarioCreacion = usuarioCreacion;
        UsuarioModificacion = usuarioModificacion;
        UsuarioEliminacion = usuarioEliminacion;
    }

    public static Consultora Create(
        string? nombreConsultora, 
        string? direccionConsultora, 
        string? representanteConsultora, 
        string? rucConsultora, 
        string? correoOrganizacional, 
        int estadoConsultora, 
        DateTime fechaCreacion,
        string? usuarioCreacion
    )
    {
        var entity = new Consultora(
            nombreConsultora,
            direccionConsultora,
            representanteConsultora,
            rucConsultora,
            correoOrganizacional,
            estadoConsultora,
            fechaCreacion,
            usuarioCreacion
        );
        //entity.RaiseDomainEvent(new ConsultoraCreateDomainEvent(entity.Id));
        return entity;
    }

    public Result Update(
        long Id,
        string? nombreConsultora, 
        string? direccionConsultora, 
        string? representanteConsultora, 
        string? rucConsultora, 
        string? correoOrganizacional, 
        DateTime utcNow,
        string? usuarioModificacion
    )
    {
        NombreConsultora = nombreConsultora;
        DireccionConsultora = direccionConsultora;
        RepresentanteConsultora = representanteConsultora;
        RucConsultora = rucConsultora;
        CorreoOrganizacional = correoOrganizacional;
        FechaModificacion = utcNow;
        UsuarioModificacion = usuarioModificacion;

        //RaiseDomainEvent(new ConsultoraUpdateDomainEvent(this.Id));

        return Result.Success();
    }

    public Result Delete(DateTime utcNow, string usuarioEliminacion)
    {
        EstadoConsultora = (int)EstadosEnum.Eliminado;
        FechaEliminacion = utcNow;
        UsuarioEliminacion = usuarioEliminacion;
        return Result.Success();
    }
}
