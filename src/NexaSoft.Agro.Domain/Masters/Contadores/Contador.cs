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
        Guid id,
        string? entidad,
        string? prefijo,
        long valorActual,
        string? agrupador,        
        string? tipoDato,
        int? valorRpeticion,
        DateTime fechaCreacion
    ) : base(id, fechaCreacion)
    {
        Entidad = entidad;
        Prefijo = prefijo;
        ValorActual = valorActual;
        Agrupador = agrupador;
        TipoDato = tipoDato;
        ValorRpeticion = valorRpeticion;
    }

    public static Contador Create(
        string? entidad,
        string? prefijo,
        long valorInicial,
        string? agrupador,
        string? tipoDato,
        int? valorRpeticion,
        DateTime fechaCreacion
    )
    {
        return new Contador(
            Guid.NewGuid(),
            entidad,
            prefijo,
            valorInicial,
            agrupador,
            tipoDato,
            valorRpeticion,
            fechaCreacion
        );
    }

    public void Update(
        string? prefijo,
        string? agrupador,
        string? tipoDato,
        int? valorRpeticion,
        DateTime utcNow
    )
    {
        if (!string.IsNullOrWhiteSpace(prefijo))
            Prefijo = prefijo;

        if (!string.IsNullOrWhiteSpace(agrupador))
            Agrupador = agrupador;
        
        if (!string.IsNullOrWhiteSpace(tipoDato))
            TipoDato = tipoDato;

        ValorRpeticion = valorRpeticion;
        FechaModificacion = utcNow; 
    }


    public string Incrementar(DateTime utcNow)
    {
        ValorActual++;
        FechaModificacion = utcNow;
        var anio = DateTime.UtcNow.Year;
        if (TipoDato == "string")
            return $"{Prefijo}{anio}{ValorActual.ToString().PadLeft(ValorRpeticion ?? 0, '0')}";
        else
            return ValorActual.ToString();
    }
}
