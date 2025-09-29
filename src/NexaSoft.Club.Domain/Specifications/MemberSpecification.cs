using NexaSoft.Club.Domain.Features.Members;

namespace NexaSoft.Club.Domain.Specifications;

public class MemberSpecification : BaseSpecification<Member, MemberResponse>
{
    public BaseSpecParams SpecParams { get; }

    public MemberSpecification(BaseSpecParams specParams) : base()
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
                case "dni":
                    AddCriteria(x => x.Dni != null && x.Dni.ToLower().Contains(specParams.Search.ToLower()));
                    break;
                case "firstname":
                    AddCriteria(x => x.FirstName != null && x.FirstName.ToLower().Contains(specParams.Search.ToLower()));
                    break;
                case "lastname":
                    AddCriteria(x => x.LastName != null && x.LastName.ToLower().Contains(specParams.Search.ToLower()));
                    break;
                case "email":
                    AddCriteria(x => x.Email != null && x.Email.ToLower().Contains(specParams.Search.ToLower()));
                    break;
                case "phone":
                    AddCriteria(x => x.Phone != null && x.Phone.ToLower().Contains(specParams.Search.ToLower()));
                    break;
                case "membertypeid":
                    if (long.TryParse(specParams.Search, out var searchNumber))
                        AddCriteria(x => x.MemberTypeId == searchNumber);
                    break;
                case "statusid":
                    if (long.TryParse(specParams.Search, out var searchNumberout))
                        AddCriteria(x => x.MemberStatusId == searchNumberout);
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
            case "dniasc":
                AddOrderBy(x => x.Dni!);
                break;
            case "dnidesc":
                AddOrderByDescending(x => x.Dni!);
                break;
            default:
                AddOrderBy(x => x.Dni!);
                break;
        }
    }

      AddSelect(x => new MemberResponse(
             x.Id,
             x.Dni,
             x.FirstName,
             x.LastName,
             x.Email,
             x.Phone,
             x.Address,
             x.BirthDate,
             x.MemberTypeId,
             x.MemberType!.TypeName!,
             x.MemberStatusId,
             x.MemberStatus!.StatusName!,
             x.JoinDate,
             x.ExpirationDate,
             x.Balance,
             x.QrCode,
             x.QrExpiration,
             x.ProfilePictureUrl,
             x.EntryFeePaid,
             x.LastPaymentDate,
             x.CreatedAt,
             x.UpdatedAt,
             x.CreatedBy,
             x.UpdatedBy
       ));
   }
}
