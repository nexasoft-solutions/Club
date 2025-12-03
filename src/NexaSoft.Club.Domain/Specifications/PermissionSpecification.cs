using NexaSoft.Club.Domain.Masters.Permissions;

namespace NexaSoft.Club.Domain.Specifications;

public class PermissionSpecification: BaseSpecification<Permission, PermissionResponse>
{
    public BaseSpecParams SpecParams { get; }

    public PermissionSpecification(BaseSpecParams specParams) : base()
    {
        SpecParams = specParams;

        // Filtro por ID
        if (specParams.Id.HasValue)
        {
            AddCriteria(x => x.Id == specParams.Id.Value);
        }
        else
        {
            // Búsqueda por campos
            if (!string.IsNullOrEmpty(specParams.Search) && 
                !string.IsNullOrEmpty(specParams.SearchFields))
            {
                switch (specParams.SearchFields.ToLower())
                {
                    case "name":
                        AddCriteria(x => x.Name != null &&
                                         x.Name.ToLower().Contains(specParams.Search.ToLower()));
                        break;

                    case "description":
                        AddCriteria(x => x.Description != null &&
                                         x.Description.ToLower().Contains(specParams.Search.ToLower()));
                        break;

                    case "reference":
                        AddCriteria(x => x.Reference != null &&
                                         x.Reference.ToLower().Contains(specParams.Search.ToLower()));
                        break;

                    default:
                        Criteria = x => true;
                        break;
                }
            }

            // Paginación
            if (!specParams.NoPaging)
            {
                ApplyPaging(specParams.PageSize * (specParams.PageIndex - 1),
                            specParams.PageSize);
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

        // Select → DTO PermissionResponse
        AddSelect(x => new PermissionResponse(
            x.Id,
            x.Name,
            x.Description,
            x.Reference,
            x.Source,
            x.Action,
            x.CreatedAt,
            x.UpdatedAt,
            x.CreatedBy,
            x.UpdatedBy
        ));
    }
}
