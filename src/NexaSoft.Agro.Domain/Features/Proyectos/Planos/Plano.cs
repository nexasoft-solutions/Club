using NexaSoft.Agro.Domain.Abstractions;
using NexaSoft.Agro.Domain.Features.Proyectos.Planos.Events;
using static NexaSoft.Agro.Domain.Shareds.Enums;
using NexaSoft.Agro.Domain.Features.Proyectos.Archivos;
using NexaSoft.Agro.Domain.Masters.Consultoras.Colaboradores;

namespace NexaSoft.Agro.Domain.Features.Proyectos.Planos;

public class Plano : Entity
{
    public int EscalaId { get; private set; }
    public string? SistemaProyeccion { get; private set; }
    public string? NombrePlano { get; private set; }
    public string? CodigoPlano { get; private set; }
    public Guid ArchivoId { get; private set; }
    public Archivo? Archivo { get; private set; }
    public Guid ColaboradorId { get; private set; }
    public int EstadoPlano { get; private set; }
    public Colaborador? Colaborador { get; private set; }

    public ICollection<PlanoDetalle> Detalles { get; private set; } = new List<PlanoDetalle>();

    private Plano() { }

    private Plano(
        Guid id,
        int escalaId,
        string? sistemaProyeccion,
        string? nombrePlano,
        string? codigoPlano,
        Guid archivoId,
        Guid colaboradorId,
        int estadoPlano,
        DateTime fechaCreacion
    ) : base(id, fechaCreacion)
    {
        EscalaId = escalaId;
        SistemaProyeccion = sistemaProyeccion;
        NombrePlano = nombrePlano;
        CodigoPlano = codigoPlano;
        ArchivoId = archivoId;
        ColaboradorId = colaboradorId;
        EstadoPlano = estadoPlano;
        FechaCreacion = fechaCreacion;
    }

    public void AgregarDetalle(PlanoDetalle detalle)
    {
        Detalles.Add(detalle);
    }

    public static Plano Create(
        int escalaId,
        string? sistemaProyeccion,
        string? nombrePlano,
        string? codigoPlano,
        Guid archivoId,
        Guid colaboradorId,
        int estadoPlano,
        DateTime fechaCreacion
    )
    {
        var entity = new Plano(
            Guid.NewGuid(),
            escalaId,
            sistemaProyeccion,
            nombrePlano,
            codigoPlano,
            archivoId,
            colaboradorId,
            estadoPlano,
            fechaCreacion
        );
        entity.RaiseDomainEvent(new PlanoCreateDomainEvent(entity.Id));
        return entity;
    }

    public Result Update(
        Guid Id,
        int escalaId,
        string? sistemaProyeccion,
        string? nombrePlano,
        string? codigoPlano,
        Guid archivoId,
        Guid colaboradorId,
        DateTime utcNow
    )
    {
        EscalaId = escalaId;
        SistemaProyeccion = sistemaProyeccion;
        NombrePlano = nombrePlano;
        CodigoPlano = codigoPlano;
        ArchivoId = archivoId;
        ColaboradorId = colaboradorId;
        FechaModificacion = utcNow;

        RaiseDomainEvent(new PlanoUpdateDomainEvent(this.Id));

        return Result.Success();
    }

    public Result Delete(DateTime utcNow)
    {
        EstadoPlano = (int)EstadosEnum.Eliminado;
        FechaEliminacion = utcNow;
        return Result.Success();
    }
}
