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
                        AddCriteria(x => x.FullName != null && x.FullName.ToLower().Contains(specParams.Search.ToLower()));
                        break;
                    case "username":
                        AddCriteria(x => x.UserName != null && x.UserName.ToLower().Contains(specParams.Search.ToLower()));
                        break;
                    case "email":
                        AddCriteria(x => x.Email != null && x.Email.ToLower().Contains(specParams.Search.ToLower()));
                        break;
                    case "userdni":
                        AddCriteria(x => x.Dni != null && x.Dni.ToLower().Contains(specParams.Search.ToLower()));
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
                    AddOrderBy(x => x.FullName!);
                    break;
                case "nombrecompletodesc":
                    AddOrderByDescending(x => x.FullName!);
                    break;
                default:
                    AddOrderBy(x => x.FullName!);
                    break;
            }
        }

        AddSelect(x => new UserResponse(
               x.Id,
               x.LastName,
               x.FirstName,
               x.FullName,
               x.UserName,
               x.Email,
               x.Dni,
               x.Phone,
               x.UserTypeId,
               x.UserType != null ? x.UserType.Name : null,
               x.BirthDate!,
               x.ProfilePictureUrl,
               x.QrCode,
               x.QrExpiration,
               x.QrUrl,
               x.LastLoginDate,
               x.PasswordSetDate,
               x.CreatedAt,
               x.UpdatedAt,
               x.CreatedBy,
               x.UpdatedBy
         ));
    }
}
