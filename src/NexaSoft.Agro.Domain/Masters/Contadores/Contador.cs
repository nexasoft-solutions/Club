using NexaSoft.Agro.Domain.Abstractions;

namespace NexaSoft.Agro.Domain.Masters.Contadores;

public class Contador : Entity
{
    public string? Entidad { get; private set; }
    public string? Prefijo { get; private set; }
    public long ValorActual { get; private set; }
    public string? Agrupador { get; private set; } // opcional para agrupar por año, empresa, etc.
    public string? TipoDato { get; private set; }
    public int? ValorRpeticion { get; private set; }
    private Contador() { }

    private Contador(
        string? entidad,
        string? prefijo,
        long valorActual,
        string? agrupador,
        string? tipoDato,
        int? valorRpeticion,
        DateTime fechaCreacion,
        string? usuarioCreacion,
        string? usuarioModificacion = null,
        string? usuarioEliminacion = null
    ) : base(fechaCreacion, usuarioCreacion, usuarioModificacion, usuarioEliminacion)
    {
        Entidad = entidad;
        Prefijo = prefijo;
        ValorActual = valorActual;
        Agrupador = agrupador;
        TipoDato = tipoDato;
        ValorRpeticion = valorRpeticion;
        UsuarioCreacion = usuarioCreacion;
        UsuarioModificacion = usuarioModificacion;
        UsuarioEliminacion = usuarioEliminacion;
    }

    public static Contador Create(
        string? entidad,
        string? prefijo,
        long valorInicial,
        string? agrupador,
        string? tipoDato,
        int? valorRpeticion,
        DateTime fechaCreacion,
        string? usuarioCreacion
    )
    {
        return new Contador(
            entidad,
            prefijo,
            valorInicial,
            agrupador,
            tipoDato,
            valorRpeticion,
            fechaCreacion,
            usuarioCreacion
        );
    }

    public void Update(
        string? prefijo,
        string? agrupador,
        string? tipoDato,
        int? valorRpeticion,
        DateTime utcNow,
        string? usuarioModificacion
    )
    {
        if (!string.IsNullOrWhiteSpace(prefijo))
            Prefijo = prefijo.ToUpper();

        if (!string.IsNullOrWhiteSpace(agrupador))
            Agrupador = agrupador;

        if (!string.IsNullOrWhiteSpace(tipoDato))
            TipoDato = tipoDato;

        ValorRpeticion = valorRpeticion;
        FechaModificacion = utcNow;
        UsuarioModificacion = usuarioModificacion; 
    }


    public string Incrementar(DateTime utcNow,string usuarioModificacion)
    {
        ValorActual++;
        FechaModificacion = utcNow;
        UsuarioModificacion = usuarioModificacion;
        var anio = DateTime.UtcNow.Year;
        if (TipoDato == "string")
            return $"{Prefijo}{anio}{ValorActual.ToString().PadLeft(ValorRpeticion ?? 0, '0')}";
        else
            return ValorActual.ToString();
    }
}
