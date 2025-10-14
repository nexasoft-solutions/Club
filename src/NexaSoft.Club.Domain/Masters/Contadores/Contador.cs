using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.Masters.Contadores;

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
         DateTime createdAt,
        string? createdBy,
        string? updatedBy = null,
        string? deletedBy = null
    ) : base(createdAt, createdBy, updatedBy, deletedBy)
    {
        Entidad = entidad;
        Prefijo = prefijo;
        ValorActual = valorActual;
        Agrupador = agrupador;
        TipoDato = tipoDato;
        ValorRpeticion = valorRpeticion;
        CreatedAt = createdAt;
        CreatedBy = createdBy;
        UpdatedBy = updatedBy;
        DeletedBy = deletedBy;
    }

    public static Contador Create(
        string? entidad,
        string? prefijo,
        long valorInicial,
        string? agrupador,
        string? tipoDato,
        int? valorRpeticion,
        DateTime createdAt,
        string? createdBy
    )
    {
        return new Contador(
            entidad,
            prefijo,
            valorInicial,
            agrupador,
            tipoDato,
            valorRpeticion,
            createdAt,
            createdBy
        );
    }

    public void Update(
        string? prefijo,
        string? agrupador,
        string? tipoDato,
        int? valorRpeticion,
        DateTime utcNow,
        string? updatedBy
    )
    {
        if (!string.IsNullOrWhiteSpace(prefijo))
            Prefijo = prefijo.ToUpper();

        if (!string.IsNullOrWhiteSpace(agrupador))
            Agrupador = agrupador;

        if (!string.IsNullOrWhiteSpace(tipoDato))
            TipoDato = tipoDato;

        ValorRpeticion = valorRpeticion;
        UpdatedAt = utcNow;
        UpdatedBy = updatedBy; 
    }


    public string Incrementar(DateTime utcNow,string updatedBy,string? sufijo, bool isComprobante = false)
    {
        ValorActual++;
        UpdatedAt = utcNow;
        UpdatedBy = updatedBy;
        //var anio = DateTime.UtcNow.Year;
        if (TipoDato == "string")
        {
            var suf = sufijo ?? "";
            return $"{Prefijo}{suf}{ValorActual.ToString().PadLeft(ValorRpeticion ?? 0, '0')}";
        }
        else
            return ValorActual.ToString();
    }
}
