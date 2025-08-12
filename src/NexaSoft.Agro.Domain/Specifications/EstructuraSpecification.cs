using NexaSoft.Agro.Domain.Features.Proyectos.Estructuras;

namespace NexaSoft.Agro.Domain.Specifications;

public class EstructuraSpecification : BaseSpecification<Estructura, EstructuraResponse>
{
    public BaseSpecParams SpecParams { get; }

    public EstructuraSpecification(BaseSpecParams specParams) : base()
    {

        SpecParams = specParams;

        if (specParams.Id.HasValue)
        {
            AddCriteria(x => x.Id == specParams.Id.Value);
        }
        else
        {
            if (!string.IsNullOrEmpty(specParams.Search) && !string.IsNullOrEmpty(specParams.SearchFields))
            {
                switch (specParams.SearchFields.ToLower())
                {
                    case "tipoestructura":
                        if (long.TryParse(specParams.Search, out var searchNumber))
                            AddCriteria(x => x.TipoEstructuraId == searchNumber);
                        break;
                    case "descripcionestructura":
                        AddCriteria(x => x.DescripcionEstructura != null && x.DescripcionEstructura.ToLower().Contains(specParams.Search.ToLower()));
                        break;
                    case "subcapitulo":
                        if (Guid.TryParse(specParams.Search, out var guid))
                        {
                            AddCriteria(x => x.SubCapituloId == guid);
                        }
                        else
                        {
                            // Esto filtra todo y da cero resultados
                            AddCriteria(x => false);
                        }
                        break;
                    case "padre":
                        if (Guid.TryParse(specParams.Search, out var padreGuid))
                        {
                            AddCriteria(x => x.PadreEstructuraId == padreGuid);
                        }
                        else
                        {
                            // Esto filtra todo y da cero resultados
                            AddCriteria(x => false);
                        }
                        break;
                    default:
                        Criteria = x => true;
                        break;
                }
            }


            // Aplicar paginación
            if (!specParams.NoPaging)
            {
                ApplyPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);
            }

            // Aplicar ordenamiento
            switch (specParams.Sort)
            {
                case "tipoestructuraasc":
                    AddOrderBy(x => x.TipoEstructuraId!);
                    break;
                case "tipoestructuradesc":
                    AddOrderByDescending(x => x.TipoEstructuraId!);
                    break;
                default:
                    AddOrderBy(x => x.TipoEstructuraId!);
                    break;
            }
        }

        AddSelect(x => new EstructuraResponse(
               x.Id,
               null,
               x.NombreEstructura,
               x.DescripcionEstructura,
               x.SubCapitulo!.NombreSubCapitulo!,
               x.PadreEstructuraId,
               x.SubCapituloId,
               x.TipoEstructuraId,
               x.FechaCreacion
         ));
    }
}
