using NexaSoft.Club.Domain.Masters.Roles;

namespace NexaSoft.Club.Domain.Specifications;

public class RoleSpecification: BaseSpecification<Role, RoleResponse>
{
    public BaseSpecParams SpecParams { get; }

    public RoleSpecification(BaseSpecParams specParams) : base()
    {
        SpecParams = specParams;

        // Filtro por ID
        if (specParams.Id.HasValue)
        {
            AddCriteria(x => x.Id == specParams.Id.Value);
        }
        else
        {
            // Filtro de búsqueda
            if (!string.IsNullOrEmpty(specParams.Search) && !string.IsNullOrEmpty(specParams.SearchFields))
            {
                switch (specParams.SearchFields.ToLower())
                {
                    case "name":
                        AddCriteria(x => x.Name != null && x.Name.ToLower().Contains(specParams.Search.ToLower()));
                        break;

                    case "description":
                        AddCriteria(x => x.Description != null && x.Description.ToLower().Contains(specParams.Search.ToLower()));
                        break;

                    default:
                        Criteria = x => true;
                        break;
                }
            }

            // Paginación
            if (!specParams.NoPaging)
            {
                ApplyPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);
            }

            // Ordenamiento
            switch (specParams.Sort)
            {
                case "nameasc":
                    AddOrderBy(x => x.Name!);
                    break;

                case "namedesc":
                    AddOrderByDescending(x => x.Name!);
                    break;

                default:
                    AddOrderBy(x => x.Name!);
                    break;
            }
        }

        // Select → RoleResponse
        AddSelect(x => new RoleResponse(
            x.Id,
            x.Name,
            x.Description,
            x.CreatedAt,
            x.UpdatedAt,
            x.CreatedBy,
            x.UpdatedBy
        ));
    }
}
