using NexaSoft.Club.Domain.Masters.Users;

namespace NexaSoft.Club.Domain.Specifications;

public class UserSpecification : BaseSpecification<User, UserResponse>
{
    public BaseSpecParams SpecParams { get; }

    public UserSpecification(BaseSpecParams specParams) : base()
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
                    case "nombrecompleto":
                        AddCriteria(x => x.NombreCompleto != null && x.NombreCompleto.ToLower().Contains(specParams.Search.ToLower()));
                        break;
                    case "username":
                        AddCriteria(x => x.UserName != null && x.UserName.ToLower().Contains(specParams.Search.ToLower()));
                        break;
                    case "email":
                        AddCriteria(x => x.Email != null && x.Email.ToLower().Contains(specParams.Search.ToLower()));
                        break;
                    case "userdni":
                        AddCriteria(x => x.UserDni != null && x.UserDni.ToLower().Contains(specParams.Search.ToLower()));
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
                case "nombrecompletoasc":
                    AddOrderBy(x => x.NombreCompleto!);
                    break;
                case "nombrecompletodesc":
                    AddOrderByDescending(x => x.NombreCompleto!);
                    break;
                default:
                    AddOrderBy(x => x.NombreCompleto!);
                    break;
            }
        }

        AddSelect(x => new UserResponse(
               x.Id,
               x.UserApellidos,
               x.UserNombres,
               x.NombreCompleto,
               x.UserName,
               x.Email,
               x.UserDni,
               x.UserTelefono,
               x.CreatedAt,
               x.UpdatedAt,
               x.CreatedBy,
               x.UpdatedBy
         ));
    }
}
