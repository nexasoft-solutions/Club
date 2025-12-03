using NexaSoft.Club.Domain.Masters.Menus;

namespace NexaSoft.Club.Domain.Specifications;

public class MenuSpecification: BaseSpecification<MenuItemOption, MenuResponse>
{
    public BaseSpecParams SpecParams { get; }

    public MenuSpecification(BaseSpecParams specParams) : base()
    {
        SpecParams = specParams;

        // Buscar por Id específico
        if (specParams.Id.HasValue)
        {
            AddCriteria(x => x.Id == specParams.Id.Value);
        }
        else
        {
            // Búsqueda general por campos
            if (!string.IsNullOrEmpty(specParams.Search) && 
                !string.IsNullOrEmpty(specParams.SearchFields))
            {
                switch (specParams.SearchFields.ToLower())
                {
                    case "label":
                        AddCriteria(x => x.Label != null &&
                            x.Label.ToLower().Contains(specParams.Search.ToLower()));
                        break;

                    case "route":
                        AddCriteria(x => x.Route != null &&
                            x.Route.ToLower().Contains(specParams.Search.ToLower()));
                        break;

                    case "icon":
                        AddCriteria(x => x.Icon != null &&
                            x.Icon.ToLower().Contains(specParams.Search.ToLower()));
                        break;

                    default:
                        Criteria = x => true;
                        break;
                }
            }

            // Paginación
            if (!specParams.NoPaging)
            {
                ApplyPaging(
                    specParams.PageSize * (specParams.PageIndex - 1),
                    specParams.PageSize
                );
            }

            // Ordenamiento
            switch (specParams.Sort)
            {
                case "labelasc":
                    AddOrderBy(x => x.Label!);
                    break;

                case "labeldesc":
                    AddOrderByDescending(x => x.Label!);
                    break;

                case "routeasc":
                    AddOrderBy(x => x.Route!);
                    break;

                case "routedesc":
                    AddOrderByDescending(x => x.Route!);
                    break;

                default:
                    AddOrderBy(x => x.Label!); // default
                    break;
            }
        }

        // Proyección a MenuResponse
        AddSelect(x => new MenuResponse(
            x.Id,
            x.Label,
            x.Icon,
            x.Route,
            x.ParentId,
            null
        ));
    }
}
