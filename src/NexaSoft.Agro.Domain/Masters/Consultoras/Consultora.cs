using NexaSoft.Agro.Domain.Abstractions;
using NexaSoft.Agro.Domain.Masters.Consultoras.Events;
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
        Guid id, 
        string? nombreConsultora, 
        string? direccionConsultora, 
        string? representanteConsultora, 
        string? rucConsultora, 
        string? correoOrganizacional, 
        int estadoConsultora, 
        DateTime fechaCreacion
    ) : base(id, fechaCreacion)
    {
        NombreConsultora = nombreConsultora;
        DireccionConsultora = direccionConsultora;
        RepresentanteConsultora = representanteConsultora;
        RucConsultora = rucConsultora;
        CorreoOrganizacional = correoOrganizacional;
        EstadoConsultora = estadoConsultora;
        FechaCreacion = fechaCreacion;
    }

    public static Consultora Create(
        string? nombreConsultora, 
        string? direccionConsultora, 
        string? representanteConsultora, 
        string? rucConsultora, 
        string? correoOrganizacional, 
        int estadoConsultora, 
        DateTime fechaCreacion
    )
    {
        var entity = new Consultora(
            Guid.NewGuid(),
            nombreConsultora,
            direccionConsultora,
            representanteConsultora,
            rucConsultora,
            correoOrganizacional,
            estadoConsultora,
            fechaCreacion
        );
        entity.RaiseDomainEvent(new ConsultoraCreateDomainEvent(entity.Id));
        return entity;
    }

    public Result Update(
        Guid Id,
        string? nombreConsultora, 
        string? direccionConsultora, 
        string? representanteConsultora, 
        string? rucConsultora, 
        string? correoOrganizacional, 
        DateTime utcNow
    )
    {
        NombreConsultora = nombreConsultora;
        DireccionConsultora = direccionConsultora;
        RepresentanteConsultora = representanteConsultora;
        RucConsultora = rucConsultora;
        CorreoOrganizacional = correoOrganizacional;
        FechaModificacion = utcNow;

        RaiseDomainEvent(new ConsultoraUpdateDomainEvent(this.Id));

        return Result.Success();
    }

    public Result Delete(DateTime utcNow)
    {
        EstadoConsultora = (int)EstadosEnum.Eliminado;
        FechaEliminacion = utcNow;
        return Result.Success();
    }
}
