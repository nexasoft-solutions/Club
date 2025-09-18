using NexaSoft.Agro.Domain.Features.Proyectos.Capitulos;

namespace NexaSoft.Agro.Domain.Specifications;

public class CapituloSpecification : BaseSpecification<Capitulo, CapituloResponse>
{
    public BaseSpecParams SpecParams { get; }

    public CapituloSpecification(BaseSpecParams specParams) : base()
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
                    case "nombrecapitulo":
                        AddCriteria(x => x.NombreCapitulo != null && x.NombreCapitulo.ToLower().Contains(specParams.Search.ToLower()));
                        break;
                    case "descripcioncapitulo":
                        AddCriteria(x => x.DescripcionCapitulo != null && x.DescripcionCapitulo.ToLower().Contains(specParams.Search.ToLower()));
                        break;
                    case "estudioambiental":
                        if (long.TryParse(specParams.Search, out var longx))
                        {
                            AddCriteria(x => x.EstudioAmbientalId == longx);
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
                case "nombrecapituloasc":
                    AddOrderBy(x => x.NombreCapitulo!);
                    break;
                case "nombrecapitulodesc":
                    AddOrderByDescending(x => x.NombreCapitulo!);
                    break;
                default:
                    AddOrderBy(x => x.NombreCapitulo!);
                    break;
            }
        }

        AddSelect(x => new CapituloResponse(
               x.Id,
               x.NombreCapitulo,
               x.DescripcionCapitulo,
               x.EstudioAmbiental!.Proyecto,
               x.EstudioAmbientalId,
               x.FechaCreacion,
               x.FechaModificacion,
               x.UsuarioCreacion,
               x.UsuarioModificacion
         ));
    }
}
