using NexaSoft.Club.Domain.Masters.UserTypes;

namespace NexaSoft.Club.Domain.Specifications;

public class UserTypeSpecification : BaseSpecification<UserType, UserTypeResponse>
{
    public BaseSpecParams SpecParams { get; }

    public UserTypeSpecification(BaseSpecParams specParams) : base()
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
                case "name":
                    AddCriteria(x => x.Name != null && x.Name.ToLower().Contains(specParams.Search.ToLower()));
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

      AddSelect(x => new UserTypeResponse(
             x.Id,
             x.Name,
             x.Description,
             x.IsAdministrative,
             x.CreatedAt,
             x.UpdatedAt,
             x.CreatedBy,
             x.UpdatedBy
       ));
   }
}
