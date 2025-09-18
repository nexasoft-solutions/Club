using NexaSoft.Agro.Domain.Abstractions;
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
    public long ArchivoId { get; private set; }
    public Archivo? Archivo { get; private set; }
    public long ColaboradorId { get; private set; }
    public int EstadoPlano { get; private set; }
    public Colaborador? Colaborador { get; private set; }

    public ICollection<PlanoDetalle> Detalles { get; private set; } = new List<PlanoDetalle>();

    private Plano() { }

    private Plano(
        int escalaId,
        string? sistemaProyeccion,
        string? nombrePlano,
        string? codigoPlano,
        long archivoId,
        long colaboradorId,
        int estadoPlano,
        DateTime fechaCreacion,
        string usuarioCreacion
    ) : base(fechaCreacion, usuarioCreacion)
    {
        EscalaId = escalaId;
        SistemaProyeccion = sistemaProyeccion;
        NombrePlano = nombrePlano;
        CodigoPlano = codigoPlano;
        ArchivoId = archivoId;
        ColaboradorId = colaboradorId;
        EstadoPlano = estadoPlano;
        FechaCreacion = fechaCreacion;
        UsuarioCreacion = usuarioCreacion;
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
        long archivoId,
        long colaboradorId,
        int estadoPlano,
        DateTime fechaCreacion,
        string usuarioCreacion
    )
    {
        var entity = new Plano(
            escalaId,
            sistemaProyeccion,
            nombrePlano,
            codigoPlano,
            archivoId,
            colaboradorId,
            estadoPlano,
            fechaCreacion,
            usuarioCreacion
        );
        //entity.RaiseDomainEvent(new PlanoCreateDomainEvent(entity.Id));
        return entity;
    }

    public Result Update(
        long Id,
        int escalaId,
        string? sistemaProyeccion,
        string? nombrePlano,
        string? codigoPlano,
        long archivoId,
        long colaboradorId,
        DateTime utcNow,
        string? usuarioModificacion
    )
    {
        EscalaId = escalaId;
        SistemaProyeccion = sistemaProyeccion;
        NombrePlano = nombrePlano;
        CodigoPlano = codigoPlano;
        ArchivoId = archivoId;
        ColaboradorId = colaboradorId;
        FechaModificacion = utcNow;
        UsuarioModificacion = usuarioModificacion;
        

        //RaiseDomainEvent(new PlanoUpdateDomainEvent(this.Id));

        return Result.Success();
    }

    public Result Delete(DateTime utcNow)
    {
        EstadoPlano = (int)EstadosEnum.Eliminado;
        FechaEliminacion = utcNow;
        return Result.Success();
    }
}
