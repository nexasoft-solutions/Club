using NexaSoft.Club.Domain.Masters.SystemUsers;

namespace NexaSoft.Club.Domain.Specifications;

public class SystemUserSpecification : BaseSpecification<SystemUser, SystemUserResponse>
{
    public BaseSpecParams SpecParams { get; }

    public SystemUserSpecification(BaseSpecParams specParams) : base()
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
                case "username":
                    AddCriteria(x => x.Username != null && x.Username.ToLower().Contains(specParams.Search.ToLower()));
                    break;
                case "email":
                    AddCriteria(x => x.Email != null && x.Email.ToLower().Contains(specParams.Search.ToLower()));
                    break;
                case "firstname":
                    AddCriteria(x => x.FirstName != null && x.FirstName.ToLower().Contains(specParams.Search.ToLower()));
                    break;
                case "lastname":
                    AddCriteria(x => x.LastName != null && x.LastName.ToLower().Contains(specParams.Search.ToLower()));
                    break;
                case "role":
                    AddCriteria(x => x.Role != null && x.Role.ToLower().Contains(specParams.Search.ToLower()));
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
            case "usernameasc":
                AddOrderBy(x => x.Username!);
                break;
            case "usernamedesc":
                AddOrderByDescending(x => x.Username!);
                break;
            default:
                AddOrderBy(x => x.Username!);
                break;
        }
    }

      AddSelect(x => new SystemUserResponse(
             x.Id,
             x.Username,
             x.Email,
             x.FirstName,
             x.LastName,
             x.Role,
             x.IsActive,
             x.CreatedAt,
             x.UpdatedAt,
             x.CreatedBy,
             x.UpdatedBy
       ));
   }
}
