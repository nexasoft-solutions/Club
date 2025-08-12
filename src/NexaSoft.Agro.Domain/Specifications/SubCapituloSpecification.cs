using NexaSoft.Agro.Domain.Features.Proyectos.SubCapitulos;

namespace NexaSoft.Agro.Domain.Specifications;

public class SubCapituloSpecification : BaseSpecification<SubCapitulo, SubCapituloResponse>
{
    public BaseSpecParams SpecParams { get; }

    public SubCapituloSpecification(BaseSpecParams specParams) : base()
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
                    case "nombresubcapitulo":
                        AddCriteria(x => x.NombreSubCapitulo != null && x.NombreSubCapitulo.ToLower().Contains(specParams.Search.ToLower()));
                        break;
                    case "descripcionsubcapitulo":
                        AddCriteria(x => x.DescripcionSubCapitulo != null && x.DescripcionSubCapitulo.ToLower().Contains(specParams.Search.ToLower()));
                        break;
                    case "capitulo":
                        if (Guid.TryParse(specParams.Search, out var guid))
                        {
                            AddCriteria(x => x.CapituloId == guid);
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
                case "nombresubcapituloasc":
                    AddOrderBy(x => x.NombreSubCapitulo!);
                    break;
                case "nombresubcapitulodesc":
                    AddOrderByDescending(x => x.NombreSubCapitulo!);
                    break;
                default:
                    AddOrderBy(x => x.NombreSubCapitulo!);
                    break;
            }
        }

        AddSelect(x => new SubCapituloResponse(
               x.Id,
               x.NombreSubCapitulo,
               x.DescripcionSubCapitulo,
               x.Capitulo!.NombreCapitulo!,
               x.CapituloId,
               x.FechaCreacion
         ));
    }
}
